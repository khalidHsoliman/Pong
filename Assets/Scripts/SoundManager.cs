using UnityEngine;


/// <summary>
/// Soundmanager to handle all SFX in the game
/// </summary>
public class SoundManager : MonoBehaviour {

    // Singleton as the gamemanager
    public static SoundManager SM = null; 

    // audioclips
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

        // referncing the audio source on that object to play SFX on it
        audioSource = GetComponent<AudioSource>(); 
    }

    /// <summary>
    /// a function to be called from any other object in the game
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip); 
    }
}
