using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bullet;
    
    public float travelspeed;
    public float rotationSpeed;
    public float lifeTimer = 5;
    public float rotatetimer;
    public bool exploding;
    public GameObject Explosion;
    public float damage;

    private AudioSource audioSource;
    public AudioClip explosionSound;
    private float curTimer;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curTimer += Time.deltaTime;
        if (curTimer > lifeTimer)
        {
            Destroy(gameObject);
        }
        // Bullet traveling foward in an constant speed
        transform.position += transform.right * Time.deltaTime * travelspeed;
        if (curTimer < rotatetimer)
        {
            transform.Rotate(0,0,rotationSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (exploding)
        {
            Instantiate(Explosion,gameObject.transform.position,Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
