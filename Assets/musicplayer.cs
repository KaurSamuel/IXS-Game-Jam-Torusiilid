using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip Music;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Music;
        audioSource.loop = true;
        audioSource.volume = ScoreHolder.Volume;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
