using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.iOS;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyInstances;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI ingredientGetText;
    [SerializeField] private ParticleSystem poofParticles;

    private int currentSlime;
    private bool enemyActive = false;
    private Coroutine textCoroutine;
    [HideInInspector] public Ingredients selectedIngredient;

    public void SpawnEnemy()
    {
        if (!enemyActive)
        {
            currentSlime = Random.Range(0, 3);
            enemyInstances[currentSlime].SetActive(true);
        }
    }

    public void KillEnemy()
    {
        if (EventSystem.current.currentSelectedGameObject.TryGetComponent<AttackButton>(out AttackButton button))
        {
            int element = button.GetElement();
            bool success = false;

            switch (element)
            {
                case 0:
                    if(currentSlime == 2)
                    {
                        success = true;
                    }
                    break;

                case 1:
                    if (currentSlime == 0)
                    {
                        success = true;
                    }
                    break;

                case 2:
                    if (currentSlime == 1)
                    {
                        success = true;
                    }
                    break;
            }
            if (success)
            {
                if (textCoroutine != null)
                {
                    StopCoroutine(textCoroutine);
                }
                ingredientGetText.text = ($"You got 1 {selectedIngredient.GetIngredientName()}!");
                textCoroutine = StartCoroutine(displayText());
                poofParticles.Play();

                inventory.AddIngredient(selectedIngredient, 1);
                enemyInstances[currentSlime].SetActive(false);
            }
        }
    }

    private IEnumerator displayText()
    {
        ingredientGetText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        ingredientGetText.gameObject.SetActive(false);
        textCoroutine = null;
    }
}
