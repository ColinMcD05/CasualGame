using UnityEngine;
using UnityEngine.UI;

public class LockLevels : MonoBehaviour
{
    public bool canUnlock;
    GameManager gameManager;

    [SerializeField] Button button;
    [SerializeField] Image lockSprite;

    [SerializeField] int levelIndex;

    public void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        if (gameManager.GetIsUnlocked(levelIndex))
        {
            button.interactable = true;
            lockSprite.enabled = false;
        }
        else
        {
            button.interactable = false;
            lockSprite.enabled = true;
        }
    }

    public void Unlock()
    {
        button.interactable = true;
        lockSprite.enabled = false;
    }
}