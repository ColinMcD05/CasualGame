using System.Collections.Generic;
using System.Security.Policy;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Menu))]
public class MenuEditor : Editor
{
    private string recipeName;
    private Ingredients[] ingredients;
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
        //ingredients = EditorGUILayout.



        if (GUILayout.Button("Make Recipe"))
        {
            MakeRecipe();
        }

        serializedObject.ApplyModifiedProperties();
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
}