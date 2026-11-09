using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PauseFunctions : MonoBehaviour
{
    [SerializeField] Toggle musicToggle, sfxToggle;

    [SerializeField] AudioMixer musicMixer, sfxMixer;

    bool musicToggled;

    GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    public void Start()
    {
        musicToggled = gameManager.musicPlaying;

        musicToggle.SetIsOnWithoutNotify(musicToggled);
        if(!musicToggle.isOn)
        {
            musicMixer.SetFloat("Volume", -80);
        }
        else
        {
            musicMixer.SetFloat("Volume", 0);
        }

        /*
        sfxToggle.isOn = gameManager.sfxPlaying;
        if (!sfxToggle.isOn)
        {
            sfxMixer.SetFloat("Master", -80);
        }
        else
        {
            sfxMixer.SetFloat("Master", 0);
        }
        */
    }

    public void MuteMusic()
    {
        musicToggled = !musicToggled;
        gameManager.musicPlaying = musicToggled;

        musicToggle.SetIsOnWithoutNotify(musicToggled);

        if (!musicToggle.isOn)
        {
            musicMixer.SetFloat("Volume", -80);
        }
        else
        {
            musicMixer.SetFloat("Volume", 0);
        }
    }

    public void MuteSFX()
    {
        sfxToggle.isOn = !sfxToggle.isOn;
    }
}