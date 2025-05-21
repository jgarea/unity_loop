using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip beberPocion;
   
    public void PlayBeberPocionSound()
    {
        if (audioSource != null && beberPocion != null)
        {
            
            audioSource.PlayOneShot(beberPocion,0.5f);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned.");
        }
    }

}
