using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    [Tooltip("Reference to the master item table")]
    [SerializeField]
    protected ItemTable masterItemTable;

    [Header("Shop")]
    [Tooltip("Inventory panels")]
    [SerializeField] private List<GameObject> inventoryPanel;
    [Tooltip("Item slot template")]
    [SerializeField] protected ShopSlot itemTemplate;
    [Tooltip("Inventory type")]
    [SerializeField] protected List<ItemType> inventoryType;

    private Dictionary<ItemType, GameObject> itemTypeToInventoryTable;
    private Dictionary<ItemType, List<ShopSlot>> itemTypeToSlotTable;
    private List<List<ShopSlot>> itemSlots;

    // Start is called before the first frame update
    void Start()
    {
        InitShop();
    }

    private void InitShop()
    {
        itemTypeToInventoryTable = new Dictionary<ItemType, GameObject>();
        itemTypeToSlotTable = new Dictionary<ItemType, List<ShopSlot>>();

        for (int i = 0; i < inventoryType.Count; i++)
        {
            itemTypeToInventoryTable[inventoryType[i]] = inventoryPanel[i];
            itemTypeToSlotTable[inventoryType[i]] = new List<ShopSlot>();
        }

        int seedCount = 0;
        int towerCount = 0;

        // Sort the items into seeds and towers and organize them into the correct inventories
        foreach (Item item in masterItemTable.items)
        {
            SetupSlot(item, itemTypeToInventoryTable[item.Type], itemTypeToSlotTable[item.Type]);

            //switch (item.Type)
            //{
            //    case ItemType.SEED:
            //        seedCount++;
            //        SetupSlot(item, itemTypeToInventoryTable[ItemType.SEED], itemTypeToSlotTable[ItemType.SEED], seedCount);
            //        break;
            //    case ItemType.TOWER:
            //        towerCount++;
            //        SetupSlot(item, itemTypeToInventoryTable[ItemType.TOWER], itemTypeToSlotTable[ItemType.TOWER], towerCount);
            //        break;
            //}
        }
    }

    private void SetupSlot(Item item, GameObject parentPanel, List<ShopSlot> slots)
    {
        GameObject newObject = Instantiate(itemTemplate.gameObject, parentPanel.transform);
        ShopSlot slot = newObject.GetComponent<ShopSlot>();
        slot.shop = this;
        slots.Add(slot);

        slot.shop = this;
        slot.SetContents(item, 1);

        //ShopSlot slot;

        //// Generate more slots as needed
        //if (slotCount > slots.Count)
        //{
        //    GameObject newObject = Instantiate(itemTemplate.gameObject, parentPanel.transform);
        //    slot = newObject.GetComponent<ShopSlot>();
        //    slot.shop = this;
        //    slots.Add(slot);
        //}
        //else
        //{
        //    slot = slots[slotCount - 1];
        //}

        //slot.shop = this;
        //slot.SetContents(item, 1);
    }

    public void SellItem(Item item)
    {
        GameStats.Instance.UseResources(item.ResourceCost1, item.ResourceCost2, item.ResourceCost3, item.ResourceCost4);
        InventoryController.Instance.AddToInventory(item);
    }
}
