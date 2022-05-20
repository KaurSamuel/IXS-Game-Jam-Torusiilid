using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bullet;
    
    public float travelspeed;
    public float rotationSpeed;

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
        if (curTimer > 5)
        {
            Destroy(gameObject);
        }
        // Bullet traveling foward in an constant speed
        transform.position += transform.right * Time.deltaTime * travelspeed;
        transform.Rotate(0,0,rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
