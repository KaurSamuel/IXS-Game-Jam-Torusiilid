using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public AudioClip JumpSound;
    public AudioClip FootStepSound;
    public AudioClip DamageSound;

    public AudioSource audioSource;
    public AudioSource walkaudioSource;

    public Animator Fade;

    private bool InvincibilityFrames;
    public float invincibilityFramesDuration = 1;
    private float CurInvTimer = 0;
    private bool walking;
    private bool gameover = false;

    private Animator animator;
    private float curGamovertimer = 0;

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
        if (gameover)
        {
            curGamovertimer += Time.deltaTime;
            if (curGamovertimer > 2.5f)
            {
                SceneManager.LoadScene("GameOver");
            }
            return;
        }
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        animator.SetFloat("Walking",Mathf.Abs(player.velocity.x));
        if (Mathf.Abs(player.velocity.x) > 1 && isTouchingGround)
        {
            walkaudioSource.volume = 0.2f;
        }
        else
        {
            walkaudioSource.volume = Single.MinValue;
        }
        
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
            audioSource.clip = JumpSound;
            audioSource.Play();
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

        if(Heart1.GetComponent<Image>() != null)
        {
            if(HP == 5)
            {
                Heart1.GetComponent<Image>().enabled = true;
                Heart2.GetComponent<Image>().enabled = true;
                Heart3.GetComponent<Image>().enabled = true;
                Heart4.GetComponent<Image>().enabled = true;
                Heart5.GetComponent<Image>().enabled = true;
            }else if(HP == 4)
            {
                Heart1.GetComponent<Image>().enabled = true;
                Heart2.GetComponent<Image>().enabled = true;
                Heart3.GetComponent<Image>().enabled = true;
                Heart4.GetComponent<Image>().enabled = true;
                Heart5.GetComponent<Image>().enabled = false;
            }else if(HP == 3)
            {
                Heart1.GetComponent<Image>().enabled = true;
                Heart2.GetComponent<Image>().enabled = true;
                Heart3.GetComponent<Image>().enabled = true;
                Heart4.GetComponent<Image>().enabled = false;
                Heart5.GetComponent<Image>().enabled = false;
            }else if(HP == 2)
            {
                Heart1.GetComponent<Image>().enabled = true;
                Heart2.GetComponent<Image>().enabled = true;
                Heart3.GetComponent<Image>().enabled = false;
                Heart4.GetComponent<Image>().enabled = false;
                Heart5.GetComponent<Image>().enabled = false;
            }else if(HP == 1)
            {
                Heart1.GetComponent<Image>().enabled = true;
                Heart2.GetComponent<Image>().enabled = false;
                Heart3.GetComponent<Image>().enabled = false;
                Heart4.GetComponent<Image>().enabled = false;
                Heart5.GetComponent<Image>().enabled = false;
            }else
            {
                Heart1.GetComponent<Image>().enabled = false;
                Heart2.GetComponent<Image>().enabled = false;
                Heart3.GetComponent<Image>().enabled = false;
                Heart4.GetComponent<Image>().enabled = false;
                Heart5.GetComponent<Image>().enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet") ) && !InvincibilityFrames)
        {
            animator.SetTrigger("Damage");
            audioSource.clip = DamageSound;
            audioSource.Play();
            HP -= 1;
            if (HP <= 0)
            {
                Gameover();
            }
            animator.SetBool("Invincibility", true);
            InvincibilityFrames = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D entered)
    {
        if(entered.gameObject.tag == "Spike")
        {
            //Debug.Log("Spike hit");
            animator.SetTrigger("Damage");
            audioSource.clip = DamageSound;
            audioSource.Play();
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
        Fade.SetTrigger("Fade");
        gameover = true;
        Debug.Log("Game Over!");
    }
}
