using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemException : System.Exception
{
    public ItemException(string message) : base(message)
    {

    }
}


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField]
    private int itemID;

    public int ItemID
    {
        get { return itemID; }
        set {
            itemID = value;
            throw new ItemException("You never should have come here!");
        }
    }

    [SerializeField]
    private new string name = "item";
    public string Name
    {
        get { return name; }
        private set { }
    }

    [SerializeField]
    [TextArea]
    private string description = "this is an item";
    public string Description
    {
        get { return description; }
        private set { }
    }

    [SerializeField]
    private string category = "misc";
    public string Category
    {
        get { return category; }
        private set { }
    }


    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
        private set { }
    }

    [SerializeField]
    private ItemType type;
    public ItemType Type
    {
        get { return type; }
        private set { }
    }

    [SerializeField]
    private string itemObjectTag;
    public string ItemObjectTag
    {
        get { return itemObjectTag; }
        private set { }
    }

    [SerializeField]
    private int resourceCost1;
    public int ResourceCost1
    {
        get { return resourceCost1; }
        private set { }
    }

    [SerializeField]
    private int resourceCost2;
    public int ResourceCost2
    {
        get { return resourceCost2; }
        private set { }
    }

    [SerializeField]
    private int resourceCost3;
    public int ResourceCost3
    {
        get { return resourceCost3; }
        private set { }
    }

    public void Use()
    {
        
    }
}
