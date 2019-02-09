using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager SM = null; 

    public AudioClip goalBloop;
    public AudioClip lossBuzz;
    public AudioClip hitPaddleBloop;
    public AudioClip winSound;
    public AudioClip wallBloop;

    private AudioSource audioSource;

    private void Start()
    {
        // Singleton 
        if (SM == null)
            SM = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>(); 
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip); 
    }
}
