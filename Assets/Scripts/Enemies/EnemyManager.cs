using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    
    private Inventory inventory;
    [HideInInspector] public Ingredients selectedIngredient;

    public void SpawnEnemy() //change to enable enemy and toggle the different slimes on and off instead?
    {

    }

    void KillEnemy()
    {
        inventory.AddIngredient(selectedIngredient, 1);
    }
}
