using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixerGroup mixer;

    public void Start()
    {
        Invoke("AssignMixers", .01f);
    }

    public void AssignMixers()
    {
        AudioSource[] sources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource source in sources)
        {
            if (source.outputAudioMixerGroup != null)
                continue;

            source.outputAudioMixerGroup = mixer;
        }
    }
}
