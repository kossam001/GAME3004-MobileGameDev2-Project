using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Copied from ItemTableEditor
[CustomEditor(typeof(RecipeTable))]
public class RecipeTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RecipeTable recipeTable = (RecipeTable)target;
        if (recipeTable)
        {
            if (GUILayout.Button("Assign item IDs"))
            {
                recipeTable.AssignItemIDs();
            }
        }
    }
}
