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
    [SerializeField] protected List<ShopSlot> towerItemSlots;

    [Header("Seed Shop")]
    [SerializeField] private GameObject seedInventoryPanel;
    [SerializeField] protected List<ShopSlot> seedItemSlots;

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

    private void SetupSlot(Item item, GameObject parentPanel, List<ShopSlot> slots, int slotCount)
    {
        ShopSlot slot;

        // Generate more slots as needed
        if (slotCount > slots.Count)
        {
            GameObject newObject = Instantiate(slots[0].gameObject, parentPanel.transform);
            slot = newObject.GetComponent<ShopSlot>();
            slots.Add(slot);
        }
        else
        {
            slot = slots[slotCount - 1];
        }

        slot.shop = this;
        slot.SetContents(item, 1);
    }

    public void SellItem(Item item)
    {
        InventoryController.Instance.AddToInventory(item);
    }
}
