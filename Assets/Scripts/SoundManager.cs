using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Instance stores a static reference to the single instance of 
    // SoundManager.
    public static SoundManager Instance = null;

    // These variables represent the clips for each sound effect in the game.
    public AudioClip gunFire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip Victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickUp;
    public AudioClip powerUpAppear;

    // Refers to the audio source added to the SoundManager.
    private AudioSource soundEffectAudio;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton pattern. Ensures there is always one copy of this 
        // object in existence. Destroys itself if there is more then one
        // in existence.
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Searches for the correct Audio Source.
        UnityEngine.AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
