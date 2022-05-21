using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum ShootingStyles
    {
        Auto,
        Semi
    }

    public float ShootingSpeed;
    public ShootingStyles ShootingStyle;
    public GameObject Bullet;
    public bool IsMuzzleFlash;
    public GameObject MuzzleFlash;
    public AudioClip ShootingSound;
    
    private AudioSource audioSource;
    public List<GameObject> Children;
    private Animator animator;
    private float curTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("ShootingSpot"))
            {
                Children.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;
        if (ShootingStyle == ShootingStyles.Auto)
        {
            if ((Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey("return")) && curTimer >= ShootingSpeed)
            {
                curTimer = 0f;
                Shoot();
            }
        }
        if (ShootingStyle == ShootingStyles.Semi)
        {
            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("return")) && curTimer >= ShootingSpeed)
            {
                curTimer = 0f;
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot");
        if (ShootingSound != null)
        {
            audioSource.clip = ShootingSound;
            audioSource.Play();
        }
        foreach (GameObject child in Children)
        {
            Instantiate(Bullet, child.transform.position, child.transform.rotation);
            if (IsMuzzleFlash)
            {
                Instantiate(MuzzleFlash, child.transform.position, child.transform.rotation);
            }
        }
    }
}
