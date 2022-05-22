using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mASTERPORTAL : MonoBehaviour
{
    public Animator fade;

    public float Curtimer;

    private bool Ending;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Ending)
        {
            Curtimer += Time.deltaTime;
            if (Curtimer > 2f)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreHolder.End = true;
            fade.SetTrigger("Fade");
            Ending = true;
        }
    }
}
