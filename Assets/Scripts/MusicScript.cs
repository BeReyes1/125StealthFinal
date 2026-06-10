using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // Static reference to the active instance
    public static MusicScript Instance { get; private set; }

    private AudioSource audioSource;

    public AudioClip menuTheme;
    public AudioClip gameTheme;
    public AudioClip loseTheme;

    private void Awake()
    {
        // Enforce the Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip) return; 
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }

}
