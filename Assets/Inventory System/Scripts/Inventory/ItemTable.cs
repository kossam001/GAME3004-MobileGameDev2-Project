﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Item Table", menuName = "ScriptableObjects/ItemTable", order = 2)]
public class ItemTable : ScriptableObject
{
    public Item[] items;
    public Item GetItem(int id)
    {
        return items[id];
    }
    public void AssignItemIDs()
    {
        for(int i = 0; i < items.Length; i++)
        {
            try
            {
                items[i].ItemID = i;
            } catch(ItemException)
            {
                // this is fine
            }
        }
    }

}
