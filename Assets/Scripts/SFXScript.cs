using UnityEngine;

public class SFXScript : MonoBehaviour
{
    // Static reference to the active instance
    public static SFXScript Instance { get; private set; }

    private AudioSource audioSource;

    public AudioClip alert;
    public AudioClip pickup;
    public AudioClip unlock;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEffect(AudioClip newClip)
    {
        audioSource.PlayOneShot(newClip);
    }

}
