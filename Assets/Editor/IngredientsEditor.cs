using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Ingredients))]
public class IngredientsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var name = serializedObject.FindProperty("ingredientName");

        var sprite = serializedObject.FindProperty("sprite");
        var image = serializedObject.FindProperty("image");

        EditorGUILayout.PropertyField(name);
        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(image);

        serializedObject.ApplyModifiedProperties();

        Sprite spriteRef = sprite.objectReferenceValue as Sprite;
        Image imageRef = image.objectReferenceValue as Image;

        if(spriteRef)
        {
            imageRef.sprite = spriteRef;
        }
    }
}
