using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _effectsSource;

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
}
