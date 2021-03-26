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
    [Tooltip("Item description panel")]
    [SerializeField] protected ItemDescriptionPanel descriptionPanel;
    [Tooltip("Panel for insufficient funds")]
    [SerializeField] protected ItemDescriptionPanel insufficientFundsPanel;

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
        }
    }

    private void SetupSlot(Item item, GameObject parentPanel, List<ShopSlot> slots)
    {
        GameObject newObject = Instantiate(itemTemplate.gameObject, parentPanel.transform);
        ShopSlot slot = newObject.GetComponent<ShopSlot>();

        slot.shop = this;
        slots.Add(slot);
        slot.SetContents(item, 1);
        slot.descriptionPanel = descriptionPanel;
    }

    public void SellItem(Item item)
    {
        if (GameStats.Instance.UseResources(item.ResourceCost1, item.ResourceCost2, item.ResourceCost3, item.ResourceCost4))
        {
            InventoryController.Instance.AddToInventory(item);
            StatisticsTracker.Instance.UpdateStats(0, 1);
        }
        else
            insufficientFundsPanel.gameObject.SetActive(true);
    }
}
