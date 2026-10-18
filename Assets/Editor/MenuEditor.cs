using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Menu))]
public class MenuEditor : Editor
{
    private string recipeName;
    private List<GameObject> ingredients = new();
    private int stirAmount;
    private bool isBossDish;
    private GameObject finishedDish;
    SerializedProperty recipeList;
    SerializedProperty bossRecipe;
    Vector2 scrollPosition;
    bool recipeInAlready;

    private void OnEnable()
    {
        recipeList = serializedObject.FindProperty("menu");
        bossRecipe = serializedObject.FindProperty("bossRecipe");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("New recipe creator");

        recipeName = EditorGUILayout.TextField("Recipe Name", recipeName);
        if (recipeInAlready)
        {
            EditorGUILayout.HelpBox("Recipe is already in.", MessageType.Error);
        }

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

        stirAmount = EditorGUILayout.IntField("Amount to stir", stirAmount);

        finishedDish = (GameObject)EditorGUILayout.ObjectField($"Finished Dish", finishedDish, typeof(GameObject), false);
        if(finishedDish && !finishedDish.GetComponent<FinishedMeal>())
        {
            finishedDish = null;
        }

        if (GUILayout.Button("Make Recipe"))
        {
            MakeRecipe();
        }
        if (GUILayout.Button("Add Stir Amount to Recipe"))
        {
            AddStirAmount();
        }

        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Menu:");
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));

        ShowRecipe();
        ShowBossRecipe();

        EditorGUILayout.EndScrollView();
    }

    public void MakeRecipe()
    {
        recipeInAlready = false;
        int menuLength = recipeList.arraySize;

        for (int i = 0; i < menuLength; i++)
        {
            SerializedProperty foundRecipe = recipeList.GetArrayElementAtIndex(i);
            SerializedProperty foundNameProperty = foundRecipe.FindPropertyRelative("recipeName");

            if (foundNameProperty.stringValue == recipeName)
            {
                recipeInAlready = true;
                return;
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

        SerializedProperty stirAmount = newRecipe.FindPropertyRelative("stirAmount");
        stirAmount.intValue = this.stirAmount;

        recipeName = string.Empty;
        ingredients.Clear();
        this.stirAmount = 0;
        finishedDish = null;
    }

    public void ShowRecipe()
    {
        for(int i = 0; i < recipeList.arraySize; i++)
        {
            SerializedProperty recipe = recipeList.GetArrayElementAtIndex(i);

            SerializedProperty recipeName = recipe.FindPropertyRelative("recipeName");

            SerializedProperty ingredients = recipe.FindPropertyRelative("ingredients");

            SerializedProperty stirAmount = recipe.FindPropertyRelative("stirAmount");

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(recipeName.stringValue, GUILayout.Width(100));
            EditorGUILayout.LabelField($"Stir amount: {stirAmount.intValue}", GUILayout.Width(100));

            if(bossRecipe.FindPropertyRelative("recipeName").stringValue == "")
            {
                if (GUILayout.Button("Make Boss Recipe", GUILayout.Width(125)))
                {
                    SwapRecipe(ref bossRecipe, ref recipe);
                    recipeList.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    i--;
                    continue;
                }
            }
            else
            {
                if (GUILayout.Button("Replace Boss Recipe", GUILayout.Width(125)))
                {
                    SwapRecipe(ref bossRecipe, ref recipe, true);
                    serializedObject.ApplyModifiedProperties();
                }
            }

            if (GUILayout.Button("-", GUILayout.Width(25)))
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
    }

    public void ShowBossRecipe()
    {
        if(bossRecipe.FindPropertyRelative("recipeName").stringValue != "")
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Boss Recipe:");

            SerializedProperty recipe = bossRecipe;

            SerializedProperty recipeName = recipe.FindPropertyRelative("recipeName");

            SerializedProperty ingredients = recipe.FindPropertyRelative("ingredients");

            SerializedProperty stirAmount = recipe.FindPropertyRelative("stirAmount");

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(recipeName.stringValue, GUILayout.Width(100));
            EditorGUILayout.LabelField($"Stir amount: {stirAmount.intValue}", GUILayout.Width(100));
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
    }

    public void AddStirAmount()
    {
        int menuLength = recipeList.arraySize;
        for (int i = 0; i < menuLength; i++)
        {
            SerializedProperty foundRecipe = recipeList.GetArrayElementAtIndex(i);
            SerializedProperty foundNameProperty = foundRecipe.FindPropertyRelative("recipeName");

            if (foundNameProperty.stringValue == recipeName)
            {
                SerializedProperty stirAmount = foundRecipe.FindPropertyRelative("stirAmount");
                stirAmount.intValue = this.stirAmount;
                return;
            }
        }
    }

    public void SwapRecipe(ref SerializedProperty to, ref SerializedProperty from, bool fullSwap = false)
    {
        string tempName = to.FindPropertyRelative("recipeName").stringValue;
        int tempStir = to.FindPropertyRelative("stirAmount").intValue;
        Object tempObject = to.FindPropertyRelative("mealPrefab").objectReferenceValue;

        var toIngredients = to.FindPropertyRelative("ingredients");
        var fromIngredients = from.FindPropertyRelative("ingredients");
        Object[] tempIngredients = new Ingredients[toIngredients.arraySize];

        for(int i = 0; i < toIngredients.arraySize; i++)
        {
            tempIngredients[i] = toIngredients.GetArrayElementAtIndex(i).objectReferenceValue;
        }

        to.FindPropertyRelative("recipeName").stringValue = from.FindPropertyRelative("recipeName").stringValue;
        to.FindPropertyRelative("stirAmount").intValue = from.FindPropertyRelative("stirAmount").intValue;
        to.FindPropertyRelative("mealPrefab").objectReferenceValue = from.FindPropertyRelative("mealPrefab").objectReferenceValue;
        toIngredients.ClearArray();

        for(int i = 0; i < fromIngredients.arraySize; i++)
        {
            toIngredients.InsertArrayElementAtIndex(i);
            toIngredients.GetArrayElementAtIndex(i).objectReferenceValue = fromIngredients.GetArrayElementAtIndex(i).objectReferenceValue;
        }

        if (fullSwap)
        {
            from.FindPropertyRelative("recipeName").stringValue = tempName;
            from.FindPropertyRelative("stirAmount").intValue = tempStir;
            from.FindPropertyRelative("mealPrefab").objectReferenceValue = tempObject;

            fromIngredients.ClearArray();

            for(int i = 0; i< tempIngredients.Length; i++)
            {
                fromIngredients.InsertArrayElementAtIndex(i);
                fromIngredients.GetArrayElementAtIndex(i).objectReferenceValue = tempIngredients[i];
            }
        }
    }
}