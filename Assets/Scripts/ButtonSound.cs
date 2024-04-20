using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    
    public AudioClip[] sounds;
    private AudioSource audioSource;

    public Vector2 pitchVariation;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void hover()
    {
        audioSource.clip = sounds[0];
        float randomPitch = Random.Range(pitchVariation.x, pitchVariation.y);
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }

    public void click()
    {
        audioSource.clip = sounds[1];
        float randomPitch = Random.Range(0.7f, 1.3f);
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }
}
