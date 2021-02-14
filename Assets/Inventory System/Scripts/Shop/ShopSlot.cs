using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopSlot : ItemSlot
{
    public Shop shop;

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
}
