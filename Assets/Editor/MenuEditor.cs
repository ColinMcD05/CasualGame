using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Menu))]
public class MenuEditor : Editor
{
    private string recipeName;
    private List<GameObject> ingredients = new();
    private GameObject finishedDish;
    SerializedProperty recipeList;
    Vector2 scrollPosition;

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
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ingredients");

        if (GUILayout.Button("Add Ingredient To Recipe"))
        {
            ingredients.Add(null);
        }
        EditorGUILayout.EndHorizontal();

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

        finishedDish = (GameObject)EditorGUILayout.ObjectField($"Finished Dish", finishedDish, typeof(GameObject), false);
        if(finishedDish && !finishedDish.GetComponent<FinishedMeal>())
        {
            finishedDish = null;
        }

        if (GUILayout.Button("Make Recipe"))
        {
            MakeRecipe();
        }

        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Menu:");
        ShowRecipe();
    }

    public void MakeRecipe()
    {
        int menuLength = recipeList.arraySize;

        for (int i = 0; i < menuLength; i++)
        {
            SerializedProperty foundRecipe = recipeList.GetArrayElementAtIndex(i);
            SerializedProperty foundNameProperty = foundRecipe.FindPropertyRelative("recipeName");

            if (foundNameProperty.stringValue == recipeName)
            {
                EditorGUILayout.HelpBox("Recipe is already in.", MessageType.Error);
            }
        }

        recipeList.arraySize++;

        SerializedProperty newRecipe = recipeList.GetArrayElementAtIndex(menuLength);
        SerializedProperty nameProperty = newRecipe.FindPropertyRelative("recipeName");

        SerializedProperty theseIngredients = newRecipe.FindPropertyRelative("ingredients");

        theseIngredients.arraySize = ingredients.Count;

        for(int i = 0; i < ingredients.Count; i++)
        {
            theseIngredients.GetArrayElementAtIndex(i).objectReferenceValue = ingredients[i].GetComponent<Ingredients>();
        }

        SerializedProperty meal = newRecipe.FindPropertyRelative("mealPrefab");
        meal.objectReferenceValue = finishedDish;

        nameProperty.stringValue = recipeName;

        recipeName = string.Empty;
        ingredients.Clear();
    }

    public void ShowRecipe()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(100));
        for(int i = 0; i < recipeList.arraySize; i++)
        {
            SerializedProperty recipe = recipeList.GetArrayElementAtIndex(i);

            SerializedProperty recipeName = recipe.FindPropertyRelative("recipeName");

            SerializedProperty ingredients = recipe.FindPropertyRelative("ingredients");

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(recipeName.stringValue);

            if (GUILayout.Button("-"))
            {
                recipeList.DeleteArrayElementAtIndex(i);
                recipeList.serializedObject.ApplyModifiedProperties();
                i--;
                continue;
            }

            EditorGUILayout.EndHorizontal();

            for (int j = 0; j < ingredients.arraySize; j++)
            {
                SerializedProperty ingredient = ingredients.GetArrayElementAtIndex(j);

                Ingredients ingredientObject = ingredient.objectReferenceValue as Ingredients;

                if (ingredientObject != null)
                {
                    EditorGUILayout.LabelField($"  - {ingredientObject.GetIngredientName()}");
                }
            }
        }

        EditorGUILayout.EndScrollView();
    }
}