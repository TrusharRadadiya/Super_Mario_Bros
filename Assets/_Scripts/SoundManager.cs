using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private AudioSource _musicSource;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlayEffect(AudioClip clip)
    {
        if (clip == null) return;
        _effectsSource.PlayOneShot(clip);
    }

    public void StopMusic() => _musicSource.Stop();

    public void PlayMusic() => _musicSource.Play();
}
