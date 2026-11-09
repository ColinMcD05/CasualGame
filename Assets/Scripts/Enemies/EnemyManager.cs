using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.iOS;

public class EnemyManager : MonoBehaviour
{
    //logic stuff
    [SerializeField] GameObject[] enemyInstances;
    [SerializeField] private Inventory inventory;
    [HideInInspector] public Ingredients selectedIngredient;
    private int currentSlime;
    private bool enemyActive = false;

    //visual stuff
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private TextMeshProUGUI ingredientGetText;
    [SerializeField] private GameObject poofVFX;
    private Coroutine textCoroutine;
    private ParticleSystem[] poofParticleSources;

    //audio stuff
    private AudioSource audioSource;
    [SerializeField] private AudioClip spellFire;
    [SerializeField] private AudioClip spellLightning;
    [SerializeField] private AudioClip spellIce;
    [SerializeField] private AudioClip slimeDeath;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        poofParticleSources = poofVFX.GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayEntrance()
    {
        playerAnimator.Play("Player_Entrance");
    }

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

            playerAnimator.Play("Player_Attack");

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

                default:
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

                foreach(ParticleSystem particleSource in poofParticleSources)
                {
                    particleSource.Play();
                }
                
                inventory.AddIngredient(selectedIngredient, 1);
                enemyInstances[currentSlime].SetActive(false);

                currentSlime = -1; //stops phantom slime farming
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
