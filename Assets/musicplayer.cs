using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip Music;

    public bool loop = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Music;
        audioSource.loop = loop;
        audioSource.volume = ScoreHolder.Volume * 0.4f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
