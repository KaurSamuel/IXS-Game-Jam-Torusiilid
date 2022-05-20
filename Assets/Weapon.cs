using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum ShootingDirections
    {
        Forward,
        Backwards,
        Upwards
    }
    public enum ShootingStyles
    {
        Auto,
        Semi
    }

    public float ShootingSpeed;
    public ShootingStyles ShootingStyle;
    public ShootingDirections ShootingDirection;
    public GameObject Bullet;
    
    public List<GameObject> Children;
    private float curTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("ShootingSpot"))
            {
                Children.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
        foreach (GameObject child in Children)
        {
            Debug.Log("Shoot");
            Instantiate(Bullet, child.transform.position, child.transform.rotation);   
        }
    }
}
