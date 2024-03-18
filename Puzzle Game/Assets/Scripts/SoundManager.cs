using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Reference to the AudioSource component
    public AudioSource audioSource;

    // Optional: You can assign this variable in the Unity Editor
    public AudioClip OnMouseClick;
    public AudioClip ClickOnCard;
    public AudioClip CardMatch;
    public AudioClip CardMisMatch;
    // Singleton pattern
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure the AudioSource component exists, if not, add one
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Method to play an AudioClip
    public void PlayClip(AudioClip clip)
    {
        // Check if the AudioClip is not null
        if (clip != null)
        {
            // Play the AudioClip using the AudioSource
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Trying to play null AudioClip.");
        }
    }
}
