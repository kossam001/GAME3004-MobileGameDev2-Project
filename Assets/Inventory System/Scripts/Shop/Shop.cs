using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    [Tooltip("Reference to the master item table")]
    [SerializeField]
    protected ItemTable masterItemTable;

    [Header("Tower Shop")]
    [SerializeField] private GameObject towerInventoryPanel;
    [SerializeField] protected List<ItemSlot> towerItemSlots;

    [Header("Seed Shop")]
    [SerializeField] private GameObject seedInventoryPanel;
    [SerializeField] protected List<ItemSlot> seedItemSlots;

    // Start is called before the first frame update
    void Start()
    {
        InitShop();
    }

    private void InitShop()
    {
        int seedCount = 0;
        int towerCount = 0;

        // Sort the items into seeds and towers and organize them into the correct inventories
        foreach (Item item in masterItemTable.items)
        {
            switch (item.Type)
            {
                case ItemType.SEED:
                    seedCount++;
                    SetupSlot(item, seedInventoryPanel, seedItemSlots, seedCount);
                    break;
                case ItemType.TOWER:
                    towerCount++;
                    SetupSlot(item, towerInventoryPanel, towerItemSlots, towerCount);
                    break;
            }
        }
    }

    private void SetupSlot(Item item, GameObject parentPanel, List<ItemSlot> slots, int slotCount)
    {
        ItemSlot slot;

        // Generate more slots as needed
        if (slotCount > slots.Count)
        {
            GameObject newObject = Instantiate(slots[0].gameObject, parentPanel.transform);
            slot = newObject.GetComponent<ItemSlot>();
            slots.Add(slot);
        }
        else
        {
            slot = slots[slotCount - 1];
        }

        slot.SetContents(item, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
