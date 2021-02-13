using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CraftingMenu : MonoBehaviour
{
    [Tooltip("Reference to the master item table")]
    [SerializeField]
    private RecipeTable masterRecipeTable;

    [Tooltip("The object which will hold Item Slots as its direct children")]
    [SerializeField]
    private GameObject ingredientsPanel;

    [Tooltip("List size determines how many slots there will be. Contents will replaced by copies of the first element")]
    [SerializeField]
    private List<ItemSlot> ingredientsSlots;
    public List<ItemSlot> IngredientsSlots
    {
        get { return ingredientsSlots; }
    }

    [Tooltip("List size determines how many slots there will be. Contents will replaced by copies of the first element")]
    [SerializeField]
    private ItemSlot resultSlot;

    [SerializeField]
    private GameObject slotOrigin; // Item slot which generates all other item slots

    // Start is called before the first frame update
    void Start()
    {
        InitItemSlots();
        InitRecipeTable();
    }

    private void InitRecipeTable()
    {
        masterRecipeTable.InitRecipeTable();
    }

    private void InitItemSlots()
    {
        Assert.IsTrue(ingredientsSlots.Count > 0, "itemSlots was empty");
        Assert.IsNotNull(slotOrigin.GetComponent<ItemSlot>(), "Inventory is missing a prefab for itemSlots. Add it as the first element of its itemSlot list");

        // init item slots
        for (int i = 0; i < ingredientsSlots.Count; i++)
        {
            GameObject newObject = Instantiate(slotOrigin, ingredientsPanel.transform);
            ItemSlot newSlot = newObject.GetComponent<ItemSlot>();
            ingredientsSlots[i] = newSlot;
        }
        
        foreach (ItemSlot item in ingredientsSlots)
        {
            item.slotUpdated.AddListener(LookForRecipe);
        }
    }

    void LookForRecipe()
    {
        string recipeCode = "";

        // Create recipeCode
        for (int i = 0; i < ingredientsSlots.Count; i++)
        {
            if (!ingredientsSlots[i].HasItem())
            {
                recipeCode += "-1"; // For empty spaces in the manual, the sign works as a delimiter
            }
            else
            {
                recipeCode += ingredientsSlots[i].ItemInSlot.ItemID;
            }
        }

        Recipe foundRecipe = masterRecipeTable.FindRecipe(recipeCode);

        if (foundRecipe)
            resultSlot.SetContents(foundRecipe.Item, foundRecipe.Amount);
        else
            resultSlot.SetContents(null, 0);
    }

    private void OnDestroy()
    {

    }
}
