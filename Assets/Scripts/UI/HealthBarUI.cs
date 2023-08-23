using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image p1hp, p2hp;

    public float MAXHP = 100;

    //hp icon to show overhealed status
    public Image healthBuffp1, healthBuffp2;
    Color trans = Color.white;
    private void Awake()
    {
        trans.a = 0;
    }
    public void UpdateHP(float value, PlayerController pc)
    {
        if (pc.player == PlayerController.Player.P1)
        {
            if (value <= MAXHP)
            {
                p1hp.fillAmount = value / MAXHP;
                if (healthBuffp1.color.a != 0) healthBuffp1.color = trans;
            }
            else
            {
                if (healthBuffp1.color.a == 0) healthBuffp1.color = Color.white;
                p1hp.fillAmount = value / value;
            }
        }
        else if (pc.player == PlayerController.Player.P2)
        {
            if (value <= MAXHP)
            {
                p1hp.fillAmount = value / MAXHP;
                if (healthBuffp2.color.a != 0) healthBuffp2.color = trans;
            }
            else
            {
                if (healthBuffp2.color.a == 0) healthBuffp2.color = Color.white;
                p1hp.fillAmount = value / value;
            }
        }
    }

}

