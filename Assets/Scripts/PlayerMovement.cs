using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;
    public int HP;
    public GameObject equippedWeapon;
    public GameObject WeaponParent;
    
    private bool InvincibilityFrames;
    public float invincibilityFramesDuration = 1;
    private float CurInvTimer = 0;

    private Animator animator;
  

  // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Weapon"))
            {
                foreach (Transform subChild in child.transform)
                {
                    equippedWeapon = subChild.gameObject;   
                }
            }
        }
    }

  // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        animator.SetFloat("Walking",Mathf.Abs(player.velocity.x));
        
        if (direction > 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            player.velocity = new Vector2(direction * speed, player.velocity.y);
        }
        else if (direction < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            player.velocity = new Vector2(direction * speed, player.velocity.y);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButton("Jump") && isTouchingGround)
        {
            animator.SetTrigger("Jump");
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        if (InvincibilityFrames)
        {
            CurInvTimer += Time.deltaTime;
            if (CurInvTimer >= invincibilityFramesDuration)
            {
                InvincibilityFrames = false;
                animator.SetBool("Invincibility", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !InvincibilityFrames)
        {
            animator.SetTrigger("Damage");
            HP -= 1;
            if (HP <= 0)
            {
                Gameover();
            }
            animator.SetBool("Invincibility", true);
            InvincibilityFrames = true;
        }
    }

    public void ChangeWeapon(GameObject weapon)
    {
        Destroy(equippedWeapon);
        Instantiate(weapon, WeaponParent.transform);

        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Weapon"))
            {
                foreach (Transform subChild in child.transform)
                {
                    equippedWeapon = subChild.gameObject;   
                }
            }
        }
    }

    private void Gameover()
    {
        Debug.Log("Game Over!");
    }
}
