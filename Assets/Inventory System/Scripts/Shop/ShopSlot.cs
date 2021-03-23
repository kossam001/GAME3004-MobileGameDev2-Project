using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopSlot : ItemSlot
{
    public Shop shop;
    public ItemDescriptionPanel descriptionPanel;

    /* Gets a reference to the item that is to be bought and 
     * shop takes the reference and determines which inventory to send
     * the item to.
     *
     * Keeps the references in one place, so the items do not all need
     * access to inventory reference.
     * 
     * Shop genereates the items itself, so it can set up the Shop reference itself.
     */ 

    public void Buy()
    {
        shop.SellItem(ItemInSlot);
    }

    public void ShowInfo()
    {
        descriptionPanel.itemDescriptionText.text = ItemInSlot.Description;

        if (ItemInSlot.Type == ItemType.SEED)
        {
            descriptionPanel.itemDescriptionText.text += "\n Yield:";
            descriptionPanel.itemDescriptionText.text += ItemInSlot.Yield1 > 0 ? "\n" + ItemInSlot.Yield1.ToString() : "";
            descriptionPanel.itemDescriptionText.text += ItemInSlot.Yield2 > 0 ? "\n" + ItemInSlot.Yield2.ToString() : "";
            descriptionPanel.itemDescriptionText.text += ItemInSlot.Yield3 > 0 ? "\n" + ItemInSlot.Yield3.ToString() : "";
        }

        descriptionPanel.itemDescriptionText.text += "\n\n Cost:";
        descriptionPanel.itemDescriptionText.text += ItemInSlot.ResourceCost1 > 0 ? "\n" + ItemInSlot.ResourceCost1.ToString() : "";
        descriptionPanel.itemDescriptionText.text += ItemInSlot.ResourceCost2 > 0 ? "\n" + ItemInSlot.ResourceCost2.ToString() : "";
        descriptionPanel.itemDescriptionText.text += ItemInSlot.ResourceCost3 > 0 ? "\n" + ItemInSlot.ResourceCost3.ToString() : "";
        descriptionPanel.itemDescriptionText.text += ItemInSlot.ResourceCost4 > 0 ? "\n" + ItemInSlot.ResourceCost4.ToString() : "";

        descriptionPanel.gameObject.SetActive(true);
    }
}
