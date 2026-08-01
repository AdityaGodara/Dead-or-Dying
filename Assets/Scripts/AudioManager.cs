using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Background")]
    [SerializeField] private AudioClip ambience;

    [Header("Settings")]
    [SerializeField] private bool playAmbienceOnStart = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (playAmbienceOnStart && ambience != null)
        {
            PlayMusic(ambience);
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
            return;

        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayFootstep(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
            return;

        footstepSource.pitch = Random.Range(0.95f, 1.05f);

        footstepSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}