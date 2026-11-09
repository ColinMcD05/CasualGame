using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseFunctions : MonoBehaviour
{
    [SerializeField] Toggle musicToggle, sfxToggle;

    [SerializeField] AudioMixer musicMixer, sfxMixer;

    GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    public void Start()
    {
        musicToggle.isOn = gameManager.musicPlaying;
        if(!musicToggle.isOn)
        {

        }

        sfxToggle.isOn = gameManager.sfxPlaying;
        if(!musicToggle)
        {

        }
    }

    public void MuteMusic()
    {
        musicToggle.isOn = !musicToggle.isOn;

        if(musicToggle.isOn)
        {
        }
    }

    public void MuteSFX()
    {
        sfxToggle.isOn = !sfxToggle.isOn;
    }
}
