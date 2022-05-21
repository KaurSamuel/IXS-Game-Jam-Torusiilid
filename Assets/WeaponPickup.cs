using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().ChangeWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
