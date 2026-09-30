using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Menu))]
public class MenuEditor : Editor
{
    private string recipeName;
    private List<GameObject> ingredients = new();
    SerializedProperty recipeList;

    private void OnEnable()
    {
        recipeList = serializedObject.FindProperty("menu");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("New recipe creator");

        recipeName = EditorGUILayout.TextField("Recipe Name", recipeName);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Ingredients");

        if (GUILayout.Button("Add Ingredient To Recipe"))
        {
            ingredients.Add(null);
        }

        if (ingredients.Count == 0)
        {
            EditorGUILayout.LabelField("    Recipe Empty     ");
        }
        else 
        {
            for(int i = 0; i < ingredients.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                ingredients[i] = (GameObject)EditorGUILayout.ObjectField($"Ingredient {i + 1}", ingredients[i], typeof(GameObject), false);
                if (ingredients[i] && !ingredients[i].GetComponent<Ingredients>())
                {
                    ingredients[i] = null;
                }

                if(GUILayout.Button("-"))
                {
                    ingredients.RemoveAt(i);
                    i--;
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        if (GUILayout.Button("Make Recipe"))
        {
            MakeRecipe();
        }

        serializedObject.ApplyModifiedProperties();

        ShowRecipe();
    }

    public void MakeRecipe()
    {
        int menuLength = recipeList.arraySize;

        for (int i = 0; i < menuLength; i++)
        {
            SerializedProperty foundRecipe = recipeList.GetArrayElementAtIndex(menuLength);

            SerializedProperty foundNameProperty = foundRecipe.FindPropertyRelative("name");

            if (foundNameProperty.stringValue == recipeName) return;
        }

        recipeList.arraySize++;

        SerializedProperty newRecipe = recipeList.GetArrayElementAtIndex(menuLength);

        SerializedProperty nameProperty = newRecipe.FindPropertyRelative("name");

        nameProperty.stringValue = recipeName;

        recipeName = string.Empty;
    }

    public void ShowRecipe()
    {

        for(int i = 0; i < recipeList.arraySize; i++)
        {
            SerializedProperty element = recipeList.GetArrayElementAtIndex(i);
            Recipe recipe = element.objectReferenceValue as Recipe;
        }
    }
}