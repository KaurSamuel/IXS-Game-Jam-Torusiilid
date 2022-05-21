using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    
    private bool isTouchingGroundRight;
    private bool isTouchingGroundLeft;
    public Transform groundCheckRight;
    public Transform groundCheckLeft;
    public float groundCheckRadius;

    public float Speed;
    public LayerMask groundLayer;
    private Rigidbody2D Enemy;
    private Animator animator;
    public GameObject WeaponPickup;

    public float HP;
    private float direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isTouchingGroundRight = Physics2D.OverlapCircle(groundCheckRight.position, groundCheckRadius, groundLayer);
        isTouchingGroundLeft = Physics2D.OverlapCircle(groundCheckLeft.position, groundCheckRadius, groundLayer);
        if (isTouchingGroundRight == false)
        {
            direction = -1;
        }
        if (isTouchingGroundLeft == false)
        {
            direction = 1;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Enemy.velocity = new Vector2(direction * Speed, Enemy.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            animator.SetTrigger("Damage");
            HP -= other.gameObject.GetComponent<Bullet>().damage;
            if (HP <= 0)
            {
                Instantiate(WeaponPickup, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            animator.SetTrigger("Damage");
                        HP -= other.gameObject.GetComponent<ExplosionScript>().Damage;
                        if (HP <= 0)
                        {
                            Instantiate(WeaponPickup, gameObject.transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
        }
    }
}
