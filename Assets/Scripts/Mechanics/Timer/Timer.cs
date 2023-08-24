using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //UI
    public Text time_text;

    //Time
    public float timer;

    //GameController reference to lose
    GameController controller;
    private void Start()
    {
        timer = 15*60;
        controller = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        string minutes = (Mathf.RoundToInt(timer) / 60) >= 10 ? (Mathf.RoundToInt(timer) / 60).ToString() : "0" + (Mathf.RoundToInt(timer) / 60).ToString();
        string seconds = (Mathf.RoundToInt(timer) % 60) >= 10 ? (Mathf.RoundToInt(timer) % 60).ToString() : "0" + (Mathf.RoundToInt(timer) % 60).ToString();
        time_text.text = minutes + ":" + seconds;
        //if timer is 0
        if (timer <= 0)
        {
            //lose
            controller.Lose();
        }
    }
}
