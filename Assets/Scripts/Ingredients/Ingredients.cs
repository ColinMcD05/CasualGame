//using LitJson;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Ingredients : MonoBehaviour, ISaving, ILoading
{
    [Header("Ingrediant Info")]
    //Ingrediant Info
    [SerializeField] string ingredientName;
    [SerializeField] Sprite sprite;

    //References
    [Header("References")]
    [SerializeField]Image image;

#region Saving and Loading
    public bool Save(string path)
    {
        var dataPath = Path.Combine(Application.persistentDataPath, path);
        if(File.Exists(dataPath) == false) { return false; }
        var text = File.ReadAllText(dataPath);
        //var data JsonMapper.ToObject();
        return true;
    }

    public bool Load(string path)
    {
        return false;
    }
#endregion

    #region Getters
    //Getters
    public string GetIngredientName()
    {
        return ingredientName;
    }


    public Sprite GetSprite()
    {
        return sprite;
    }
    #endregion

    #region Setters
    public void SetImageColor(Color newColor)
    {
        if(image)
        {
            image.color = newColor;
        }
    }
    #endregion
}