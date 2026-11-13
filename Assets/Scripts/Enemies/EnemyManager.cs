using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyManager : MonoBehaviour
{
    [Header("Logic Stuff")]
    [SerializeField] GameObject[] enemyInstances;
    [SerializeField] private Inventory inventory;
    [HideInInspector] public Ingredients selectedIngredient;
    private int currentSlime;
    private int buttonElement;
    private bool enemyActive = false;

    [Header("Visual Stuff")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private TextMeshProUGUI ingredientGetText;
    [SerializeField] private GameObject poofVFX;
    [SerializeField] private GameObject[] spellCastPrefabs;
    [SerializeField] private GameObject[] spellProjectilePrefabs;
    [SerializeField] private GameObject[] spellCollisionPrefabs;
    private Coroutine fightCoroutine;
    private ParticleSystem[] poofParticleSources;

    [Header("Audio Stuff")]
    [SerializeField] private AudioClip[] spellSounds;
    [SerializeField] private AudioClip slimeDeath;
    private AudioSource audioSource;

    [Header("Timing Tuning")]
    [SerializeField] private float textDuration = 2f;
    [SerializeField] private float castDelay = .1f;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float collisionDelay = 1f;
    [SerializeField] private float deathDelay = .5f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        poofParticleSources = poofVFX.GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayEntrance() //if entrance is still playing when you go into the scene it won't start from the beginning
    {
        playerAnimator.Play("Player_Entrance", -1, 0);
    }

    public void SpawnEnemy()
    {
        if (!enemyActive)
        {
            currentSlime = Random.Range(0, 3);
            enemyInstances[currentSlime].SetActive(true);
            enemyActive = true;
        }
    }

    public void KillEnemy()
    {
        if (EventSystem.current.currentSelectedGameObject.TryGetComponent<AttackButton>(out AttackButton button) && currentSlime >=0)
        {
            buttonElement = button.GetElement();
            bool success = false;

            //check for the right element being used
            switch (buttonElement)
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

            //plays animations, vfx, and sounds
            playerAnimator.Play("Player_Attack");
            if (success)
            {
                if (fightCoroutine != null)
                {
                    StopCoroutine(fightCoroutine);
                }
                fightCoroutine = StartCoroutine(fightSuccess());
            }
            else
            {
                if (fightCoroutine != null)
                {
                    StopCoroutine(fightCoroutine);
                }
                fightCoroutine = StartCoroutine(fightFail());
            }
        }
    }

    private IEnumerator fightSuccess()
    {
        //plays cast particles and spawn projectile
        yield return new WaitForSeconds(castDelay);
        audioSource.PlayOneShot(spellSounds[buttonElement]);
        Instantiate(spellCastPrefabs[buttonElement], new Vector3(-51.4300003f, 4.58300018f, -6.18900013f), Quaternion.Euler(0, 90, 0));

        GameObject projectile = Instantiate(spellProjectilePrefabs[buttonElement], new Vector3(-51.4300003f, 4.58300018f, -6.18900013f), Quaternion.Euler(0, 90, 0));
        ProjectileMove projectileScript = projectile.GetComponent<ProjectileMove>();
        projectileScript.SetSpeed(projectileSpeed);
        Destroy(projectile, collisionDelay);

        //plays collision particles
        yield return new WaitForSeconds(collisionDelay);
        Instantiate(spellCollisionPrefabs[buttonElement], new Vector3(-47.4766693f, 2.68000007f, -4f), Quaternion.identity);

        //kills slime and plays death particles
        yield return new WaitForSeconds(deathDelay);
        audioSource.PlayOneShot(slimeDeath);
        enemyInstances[currentSlime].SetActive(false);
        enemyActive = false;
        foreach (ParticleSystem particleSource in poofParticleSources)
        {
            particleSource.Play();
        }

        inventory.AddIngredient(selectedIngredient, 1);
        currentSlime = -1; //stops phantom slime farming

        //display text
        ingredientGetText.text = ($"You got 1 {selectedIngredient.GetIngredientName()}!");
        ingredientGetText.gameObject.SetActive(true);
        yield return new WaitForSeconds(textDuration);
        ingredientGetText.gameObject.SetActive(false);

        fightCoroutine = null;
    }

    private IEnumerator fightFail()
    {
        //plays cast particles and spawn projectile
        yield return new WaitForSeconds(castDelay);
        audioSource.PlayOneShot(spellSounds[buttonElement]);
        Instantiate(spellCastPrefabs[buttonElement], new Vector3(-51.4300003f, 4.58300018f, -6.18900013f), Quaternion.Euler(0, 90, 0));

        GameObject projectile = Instantiate(spellProjectilePrefabs[buttonElement], new Vector3(-51.4300003f, 4.58300018f, -6.18900013f), Quaternion.Euler(0, 90, 0));
        ProjectileMove projectileScript = projectile.GetComponent<ProjectileMove>();
        projectileScript.SetSpeed(projectileSpeed);
        Destroy(projectile, collisionDelay);

        //plays collision particles
        yield return new WaitForSeconds(collisionDelay);
        Instantiate(spellCollisionPrefabs[buttonElement], new Vector3(-47.4766693f, 2.68000007f, -4f), Quaternion.identity);

        fightCoroutine = null;
    }
}
