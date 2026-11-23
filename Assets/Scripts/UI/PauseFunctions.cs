using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PauseFunctions : MonoBehaviour
{
    [SerializeField] Toggle musicToggle, sfxToggle;

    [SerializeField] AudioMixer musicMixer, sfxMixer;

    bool musicToggled, sfxToggled;

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

        sfxToggled = gameManager.sfxPlaying;

        sfxToggle.SetIsOnWithoutNotify(sfxToggled);
        if (!sfxToggle.isOn)
        {
            sfxMixer.SetFloat("Volume", -80);
        }
        else
        {
            sfxMixer.SetFloat("Volume", 0);
        }
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
        sfxToggled = !sfxToggled;
        gameManager.sfxPlaying = sfxToggled;

        sfxToggle.SetIsOnWithoutNotify(sfxToggled);

        if (!sfxToggle.isOn)
        {
            sfxMixer.SetFloat("Volume", -80);
        }
        else
        {
            sfxMixer.SetFloat("Volume", 0);
        }
    }
}