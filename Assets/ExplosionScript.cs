using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float Lifetimer = 1;
    public float Damage = 3;
    private float curtimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curtimer += Time.deltaTime;
        if (curtimer >= Lifetimer)
        {
            Destroy(gameObject);
        }
    }
}
