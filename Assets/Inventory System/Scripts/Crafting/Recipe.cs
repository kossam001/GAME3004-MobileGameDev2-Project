using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 3)]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private int recipeID;

    public int ItemID
    {
        get { return recipeID; }
        set
        {
            recipeID = value;
        }
    }

    [SerializeField]
    private new string name = "recipe";
    public string Name
    {
        get { return name; }
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
    private Item item;
    public Item Item
    {
        get { return item; }
        private set { }
    }

    // Some recipes can produce more than one item
    [SerializeField]
    private int amount = 1;
    public int Amount
    {
        get { return amount; }
        private set { }
    }

    /* Crafting grid represented by a list.
     * 
     * With a 3 x 3 crafting grid:
     * [0][1][2]
     * [3][4][5]
     * [6][7][8]
     */

    [SerializeField]
    private Item[] recipeManual;
    public Item[] RecipeManual
    {
        get { return recipeManual; }
        private set { }
    }

    // Convert recipeManual into something easier to process
    // A code created from the ingredients id
    // Saves the recipeCode on first run.  Additional items and recipes will require a reset to recalculate code.
    private string recipeCode = "";
    public string GetRecipeCode()
    {
        // Create recipeCode
        if (recipeCode == "")
        {
            for (int i = 0; i < recipeManual.Length; i++)
            {
                if (recipeManual[i] == null)
                {
                    recipeCode += "-1"; // For empty spaces in the manual, the sign works as a delimiter
                }
                else
                {
                    recipeCode += recipeManual[i].ItemID;
                }
            }
        }
        
        return recipeCode;
    }

    // Need to call this every the Item IDs are reassigned
    public void ResetRecipeCode()
    {
        recipeCode = "";
    }
}
