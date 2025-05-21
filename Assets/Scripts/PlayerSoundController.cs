using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip beberPocion;
    public AudioClip boton;
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
    public void PlayBotonSound()
    {
        if (audioSource != null && boton != null)
        {
            audioSource.PlayOneShot(boton, 0.5f);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned.");
        }
    }
    
}
