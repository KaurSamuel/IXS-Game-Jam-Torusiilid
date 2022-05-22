using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalScript : MonoBehaviour
{
    private Animator Animator;

    public GameObject FadeObject;
    public AudioClip Portaltravel;
    private AudioSource audioSource;
    private bool IsStartGame = false;
    private float Curtimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Animator = FadeObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStartGame)
        {
            Curtimer += Time.deltaTime;
            if (Curtimer > 3f)
            {
                StartGame();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip = Portaltravel;
            audioSource.Play();
            Animator.SetTrigger("Fade");
            IsStartGame = true;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Master");
    }
}
