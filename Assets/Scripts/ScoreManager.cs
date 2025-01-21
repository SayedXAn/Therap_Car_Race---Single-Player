using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int score = 0;
    public void SetScore(int x)
    {
        score = score + x;
    }
    public int GetScore()
    {
        return score;
    }
    public void ResetScore()
    {
        score=0;
    }
}
