using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopSlot : ItemSlot
{
    public Inventory playerInventory;

    public void Buy()
    {
        foreach (ItemSlot itemSlot in playerInventory.itemSlots)
        {
            if (itemSlot.AddItems(this.ItemInSlot, 1)) return;
        }
    }
}
