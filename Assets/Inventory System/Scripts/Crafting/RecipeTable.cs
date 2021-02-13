using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;



[CreateAssetMenu(fileName = "Recipe Table", menuName = "ScriptableObjects/RecipeTable", order = 4)]
public class RecipeTable : ScriptableObject
{
    [SerializeField]
    private Recipe[] recipes;
    private List<List<Recipe>> recipeTable; // A table where the key is the number of empty spaces in the recipe

    [SerializeField]
    private int ingredientSlots; // The number of itemSlots the crafting menu is using

    public void InitRecipeTable()
    {
        recipeTable = new List<List<Recipe>>();
        // + 1 for when no ingredients added - to prevent index out of range
        for (int i = 0; i < ingredientSlots+1; i++)
        {
            recipeTable.Add(new List<Recipe>());
        }

        OrganizeRecipes();
    }

    public Recipe FindRecipe(string recipeCode)
    {
        Recipe foundRecipe = null; // Chance that no recipe is found
        List<Recipe> potentialRecipes = recipeTable[Regex.Matches(recipeCode, "-").Count];

        foreach (Recipe recipe in potentialRecipes)
        {
            if (recipe.GetRecipeCode() == recipeCode)
            {
                foundRecipe = recipe;
                break; // Found a hit, return immediately
            }
        }

        return foundRecipe;
    }

    // Organize recipes based on identification for faster access
    // Regex from https://stackoverflow.com/questions/15577464/how-to-count-of-sub-string-occurrences
    private void OrganizeRecipes()
    {
        foreach (Recipe recipe in recipes)
        {
            AddRecipe(recipe);
        }
    }

    public void AddRecipe(Recipe recipe)
    {
        string recipeCode = recipe.GetRecipeCode();
        recipeTable[Regex.Matches(recipeCode, "-").Count].Add(recipe); // Add the recipe into the table based on number of empty spaces
    }

    public void AssignItemIDs()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            try
            {
                recipes[i].ItemID = i;
                recipes[i].ResetRecipeCode(); // When Item IDs get reassigned, the RecipeCode need to be recalculated
            }
            catch (ItemException)
            {
                // this is fine
            }
        }
    }

}