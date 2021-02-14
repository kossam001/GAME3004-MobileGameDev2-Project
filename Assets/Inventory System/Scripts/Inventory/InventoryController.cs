﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;
    public static InventoryController Instance { get { return instance; } }

    // Item movement variables
    private bool currentlyMovingItem = false; // Whether or not we are in the process of moving an item
    public ItemSlot cursorIcon; // Visual for moving item

    // Graphic Raycaster code from https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Code from https://forum.unity.com/threads/graphicraycaster-raycast-on-nested-canvases.603436/
            PointerEventData m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "ItemSlot")
                {
                    ItemSlot itemSlot = result.gameObject.GetComponent<ItemSlot>();

                    if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        PickUpOne(itemSlot);
                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        DropAll(itemSlot);
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        PickUpAll(itemSlot);
                    }
                    else
                    {
                        MoveItem(itemSlot);
                    }
                    break; // No point in checking the other results
                }

                Cancel();
            }
        }

        cursorIcon.transform.position = Input.mousePosition;
    }

    // Allows player to pickup one item at a time.
    public void PickUpOne(ItemSlot itemSlot)
    {
        if (itemSlot.HasItem() &&
            (!cursorIcon.HasItem() ||
            itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
        {
            currentlyMovingItem = true;

            // Increase amount being moved by 1
            cursorIcon.AddItems(itemSlot.ItemInSlot, 1);

            // Decrease items in slot by 1
            itemSlot.TryRemoveItems(1);

            if (itemSlot.GetComponent<ResultSlot>())
            {
                itemSlot.gameObject.GetComponent<ResultSlot>().ConsumeIngredients();
            }
        }
        // Has the option to swap held item and slot item, but not part of requirement
        //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
        //{
        //    SwapHeldItem(itemSlot);
        //}
    }

    // Allows the player to pickup the entire stack of items.
    public void PickUpAll(ItemSlot itemSlot)
    {
        if (itemSlot.HasItem() &&
            (!cursorIcon.HasItem() ||
            itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
        {
            currentlyMovingItem = true;

            // Amount being moved is equal to the number of items in the slot
            cursorIcon.AddItems(itemSlot.ItemInSlot, itemSlot.ItemCount);

            // Remove all items, which is the amount of items in the slot
            itemSlot.TryRemoveItems(itemSlot.ItemCount);

            if (itemSlot.GetComponent<ResultSlot>())
            {
                itemSlot.gameObject.GetComponent<ResultSlot>().ConsumeIngredients();
            }
        }
        // Has the option to swap held item and slot item, but not part of requirement
        //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
        //{
        //    SwapHeldItem(itemSlot);
        //}
    }

    // Allows player to drop one item into a slot.
    public void DropOne(ItemSlot itemSlot)
    {
        if (itemSlot.canSetSlot)
        {
            if (cursorIcon.HasItem() &&
                (!itemSlot.HasItem() ||
                itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
            {
                if (cursorIcon.TryRemoveItems(1) > 0)
                {
                    itemSlot.AddItems(cursorIcon.ItemInSlot, 1);
                }
                if (cursorIcon.ItemCount <= 0)
                {
                    currentlyMovingItem = false;
                }
            }
            // Has the option to swap held item and slot item, but not part of requirement
            //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
            //{
            //    SwapHeldItem(itemSlot);
            //}
        }
    }

    // Allows a player to drop all currently held items into a slot.
    public void DropAll(ItemSlot itemSlot)
    {
        if (itemSlot.canSetSlot)
        {
            if (cursorIcon.HasItem() &&
                (!itemSlot.HasItem() ||
                itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
            {
                if (cursorIcon.ItemCount > 0)
                {
                    itemSlot.AddItems(cursorIcon.ItemInSlot, cursorIcon.ItemCount);
                    cursorIcon.TryRemoveItems(cursorIcon.ItemCount);
                    currentlyMovingItem = false;
                }
            }
            // Has the option to swap held item and slot item, but not part of requirement
            //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
            //{
            //    SwapHeldItem(itemSlot);
            //}
        }
    }

    // Swap currently held item with the one in the slot.  Unused.
    public void SwapHeldItem(ItemSlot itemSlot)
    {
        // Swap the item in the slot with the item being transferred

        Item itemTemp = itemSlot.ItemInSlot;
        int countTemp = itemSlot.ItemCount;

        itemSlot.SetContents(cursorIcon.ItemInSlot, cursorIcon.ItemCount);
        // Swap the cursor icon
        cursorIcon.SetContents(itemTemp, countTemp);

        if (cursorIcon.HasItem())
        {
            currentlyMovingItem = true;
        }
    }

    // Bare minimum
    public void MoveItem(ItemSlot itemSlot)
    {
        if (currentlyMovingItem)
        {
            DropOne(itemSlot);
        }
        else if (!currentlyMovingItem)
        {
            // If picking up a crafted item, pickup all output
            if (itemSlot.GetComponent<ResultSlot>())
            {
                PickUpAll(itemSlot);
            }
            else
            {
                PickUpOne(itemSlot);
            }
        }
    }

    public void Cancel()
    {
        cursorIcon.TryRemoveItems(cursorIcon.ItemCount);
        currentlyMovingItem = false;
    }
}