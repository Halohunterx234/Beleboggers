using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    //Referenes
    public Slider scoreBar;

    //update the score bar
    public void UpdateScore(int score)
    {
        scoreBar.value = score;
    }
}
