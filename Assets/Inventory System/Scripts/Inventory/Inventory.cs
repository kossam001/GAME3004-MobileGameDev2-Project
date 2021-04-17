﻿using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;
using System.Linq; // For finding all gameObjects with name

public class Inventory : MonoBehaviour, ISaveHandler
{
    [Tooltip("Reference to the master item table")]
    [SerializeField]
    protected ItemTable masterItemTable;

    [Tooltip("The object which will hold Item Slots as its direct children")]
    [SerializeField]
    protected GameObject inventoryPanel;

    [Tooltip("List size determines how many slots there will be. Contents will replaced by copies of the first element")]
    public List<ItemSlot> itemSlots;

    [Tooltip("Items to add on Start for testing purposes")]
    [SerializeField]
    protected List<Item> startingItems;

    [SerializeField]
    protected ItemType inventoryType;

    private void Awake()
    {
        InitItemSlots();
        InitSaveInfo();

        // init starting items for testing
        for (int i = 0; i < startingItems.Count && i < itemSlots.Count; i++)
        {
            // Some empty slots are initialized to 16
            if (startingItems[i] != null)
                itemSlots[i].SetContents(startingItems[i], 16);
        }

        if (LoadButtonBehaviour.loadGameOnStartup == true)
            OnLoad();
    }


    protected void InitItemSlots()
    {
        Assert.IsTrue(itemSlots.Count > 0, "itemSlots was empty");
        Assert.IsNotNull(itemSlots[0], "Inventory is the first itemslot. Add it as the first element of its itemSlot list from the scene");

        // init item slots
        for (int i = 1; i < itemSlots.Count; i++)
        {
            GameObject newObject = Instantiate(itemSlots[0].gameObject, inventoryPanel.transform);
            ItemSlot newSlot = newObject.GetComponent<ItemSlot>();
            itemSlots[i] = newSlot;
        }

        foreach (ItemSlot item in itemSlots)
        {
            item.onItemUse.AddListener(OnItemUsed);
        }
    }

    private void InitSaveInfo()
    {
        //// init save info
        ////assert only one object with the same name, or else we can have key collisions on PlayerPrefs
        //Assert.AreEqual(
        //    Resources.FindObjectsOfTypeAll(typeof(GameObject)).Where(gameObArg => gameObArg.name == gameObject.name).Count(),
        //    1,
        //    "More than one gameObject have the same name, therefore there may be save key collisions in PlayerPrefs"
        //    );

        //// set a key to use for saving/loading
        //saveKey = gameObject.name + this.GetType().Name;

        ////Subscribe to save events on start so we are listening
        //GameSaver.OnLoad.AddListener(OnLoad);
        //GameSaver.OnSave.AddListener(OnSave);
    }

    private void OnDestroy()
    {
        // Remove listeners on destroy
        GameSaver.OnLoad.RemoveListener(OnLoad);
        GameSaver.OnSave.RemoveListener(OnSave);

        foreach (ItemSlot item in itemSlots)
        {
            item.onItemUse.RemoveListener(OnItemUsed);
        }
    }

    //////// Event callbacks ////////

    void OnItemUsed(Item itemUsed)
    {
       // Debug.Log("Inventory: item used of category " + itemUsed.category);
    }

    public void OnSave()
    {
        //Make empty string
        //For each item slot
        //Get its current item
        //If there is an item, write its id, and its count to the end of the string
        //If there is not an item, write -1 and 0 

        //File format:
        //Inventory Type,ID,Count,ID,Count,ID,Count

        string saveStr = "";

        foreach(ItemSlot itemSlot in itemSlots)
        {
            int id = -1;
            int count = 0;

            if(itemSlot.HasItem())
            {
                id = itemSlot.ItemInSlot.ItemID;
                count = itemSlot.ItemCount;
            } 

            saveStr += id.ToString() + ',' + count.ToString() + ',';
        }

        PlayerPrefs.SetString(inventoryType.ToString() + "inventory", saveStr);
    }

    public void OnLoad()
    {
        //Get save string
        //Split save string
        //For each itemSlot, grab a pair of entried (ID, count) and parse them to int
        //If ID is -1, replace itemSlot's item with null
        //Otherwise, replace itemSlot with the corresponding item from the itemTable, and set its count to the parsed count

        if(PlayerPrefs.HasKey(inventoryType.ToString() + "inventory"))
        {
            string loadedData = PlayerPrefs.GetString(inventoryType.ToString() + "inventory", "");

            char[] delimiters = new char[] { ',' };
            string[] splitData = loadedData.Split(delimiters);

            for (int i = 0; i < itemSlots.Count; i++)
            {
                int dataIdx = i * 2;

                int id = int.Parse(splitData[dataIdx]);
                int count = int.Parse(splitData[dataIdx + 1]);

                if (id < 0)
                {
                    itemSlots[i].ClearSlot();
                }
                else
                {
                    itemSlots[i].SetContents(masterItemTable.GetItem(id), count);
                }
            }
        }    
    }
}
