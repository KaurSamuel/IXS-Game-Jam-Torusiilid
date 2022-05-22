using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorecounter : MonoBehaviour
{
    // Start is called before the first frame update
    private int Score = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Score += 1;
    }
}
