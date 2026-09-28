using UnityEngine;
using System.Collections.Generic;

public class CookingPot : MonoBehaviour, IDroppedOn
{
    List<Ingredients> ingredientsInPot = new List<Ingredients>();

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
