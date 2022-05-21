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
    private bool isTouchingGround;
    public int HP;
    public GameObject equippedWeapon;
    public GameObject WeaponParent;

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
    void FixedUpdate()
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

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            animator.SetTrigger("Jump");
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
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
}
