using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image p1hp, p2hp;

    public float MAXHP = 100;

    public void UpdateHP(float value, PlayerController pc)
    {
        if (pc.player == PlayerController.Player.P1)
        {
            if (value <= MAXHP) p1hp.fillAmount = value / MAXHP;
            else p1hp.fillAmount = value / value;
        }
        else if (pc.player == PlayerController.Player.P2)
        {
            if (value <= MAXHP) p1hp.fillAmount = value / MAXHP;
            else p1hp.fillAmount = value / value;
        }
    }

}

