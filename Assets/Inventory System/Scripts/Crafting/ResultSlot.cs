using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Special type of ItemSlot that only allows the player to remove the items and not set items.
// Also has a unique method that reduces all the items in the crafting menu by 1.
public class ResultSlot : ItemSlot
{
    [SerializeField]
    private InventoryController inventoryController;

    [SerializeField]
    private CraftingMenu craftingMenu;

    // If a player takes out the crafted item, make sure to consume the ingredients used
    public void ConsumeIngredients()
    {
        List<ItemSlot> ingredientsSlots = craftingMenu.IngredientsSlots;

        foreach (ItemSlot ingredient in ingredientsSlots)
        {
            ingredient.TryRemoveItems(1);
        }
    }
}
