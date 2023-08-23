using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image p1hp, p2hp;

    public const float MAXHP=100;

    public void UpdateHP(float value, PlayerController pc)
    {
        print(value / MAXHP);
        if (pc.player == PlayerController.Player.P1) p1hp.fillAmount = value / MAXHP;
        else if (pc.player == PlayerController.Player.P2) p2hp.fillAmount = value / MAXHP;
    }

}

