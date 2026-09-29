using UnityEngine;
using System.Collections.Generic;

public class CookingPot : MonoBehaviour, IDroppedOn
{
    List<Ingredients> ingredientsInPot = new List<Ingredients>();

    Menu currentMenu;

    public void DroppedOn(Ingredients ingredient)
    {
        ingredientsInPot.Add(ingredient);
    }

    public void MakeMeal()
    {
        
    }

    public void EmptyPot()
    {
        ingredientsInPot.Clear();
    }
}
