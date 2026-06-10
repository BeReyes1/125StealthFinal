using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // Static reference to the active instance
    public static MusicScript Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        // Enforce the Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Delete duplicate manager in new scenes
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Prevent destruction on scene load

        audioSource = GetComponent<AudioSource>();
    }

    // Call this method from other scripts to change tracks between scenes
    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip) return; 
        
        audioSource.clip = newClip;
        audioSource.Play();
    }

}
