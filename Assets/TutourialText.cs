using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutourialText : MonoBehaviour
{

    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Animator.SetTrigger("Enter");
    }
}
