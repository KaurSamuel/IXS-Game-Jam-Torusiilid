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
    public bool Shooting;
    public float ShootingSpeed = 2;
    public bool Jumping;
    public float JumpingSpeed;
    public float JumpingForce;
    public bool Floating;
    public bool Stational;
    
    public GameObject bullet;
    public LayerMask groundLayer;
    private Rigidbody2D Enemy;
    private Animator animator;
    public GameObject WeaponPickup;
    public List<GameObject> Children;

    public float HP;
    private float direction = 1;

    private float curTimerShoot = 0;
    private float curTimerjump = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Enemy = GetComponent<Rigidbody2D>();
        
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("ShootingSpot"))
            {
                Children.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curTimerShoot += Time.deltaTime;
        curTimerjump += Time.deltaTime;
        isTouchingGroundRight = Physics2D.OverlapCircle(groundCheckRight.position, groundCheckRadius, groundLayer);
        isTouchingGroundLeft = Physics2D.OverlapCircle(groundCheckLeft.position, groundCheckRadius, groundLayer);
        if (!Floating)
        {
            if (isTouchingGroundRight == false)
            {
                direction = -1;
            }
            if (isTouchingGroundLeft == false)
            {
                direction = 1;
            } 
        }
        else
        {
            if (isTouchingGroundRight)
            {
                direction = -1;
            }
            if (isTouchingGroundLeft)
            {
                direction = 1;
            } 
        }
        
        if (Shooting && curTimerShoot >= ShootingSpeed)
        {
            Shoot();
            curTimerShoot = 0;
        }

        if (Stational)
        {
            return;
        }
        
        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
        Enemy.velocity = new Vector2(direction*-1 * Speed, Enemy.velocity.y);
        
        if (Jumping && curTimerjump >= JumpingSpeed)
        {
            Enemy.velocity = new Vector2(0, JumpingForce);
            curTimerjump = 0;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            animator.SetTrigger("Damage");
            HP -= other.gameObject.GetComponent<Bullet>().damage;
            if (HP <= 0)
            {
                ScoreHolder.EnemiesKilled += 1;
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
                ScoreHolder.EnemiesKilled += 1;
                Instantiate(WeaponPickup, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void Shoot()
    {
        foreach (GameObject child in Children)
        {
            child.tag = "EnemyBullet";
            Instantiate(bullet, child.transform.position, child.transform.rotation);
        }
    }
}
