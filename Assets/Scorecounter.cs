using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorecounter : MonoBehaviour
{
    // Start is called before the first frame update
    private int Score = 0;
    private int FinalScore = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Score += 1;
        if (ScoreHolder.End)
        {
            FinalScore = (500 - Score/60) + ScoreHolder.EnemiesKilled * 10;
        }
        else
        {
            FinalScore = ScoreHolder.EnemiesKilled * 10;   
        }
        ScoreHolder.Score = FinalScore;
    }
}
