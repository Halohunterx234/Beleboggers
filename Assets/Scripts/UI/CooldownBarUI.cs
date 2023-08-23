using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBarUI : MonoBehaviour
{
    //UI
    public Image catBasic, catSkill, chickenBasic, chickenSkill;

    //buffed color
    public Color buffedColor;

    //update the corresponding skills visuals when they are in cooldown
    public void UpdateBasic(float value, float maxcd, string type, PlayerController.Player player)
    {
        if (player == PlayerController.Player.P1)
        {
            if (type == "Basic")
            {
                //fill the radial image 
                catBasic.fillAmount = value / maxcd;

                //if the cooldown is still ongoing, shade the color
                //else reset it to zero
                catBasic.color = Color.grey;
            }
            else if (type == "Skill")
            {
                catSkill.fillAmount = value / maxcd;

                //if the cooldown is still ongoing, shade the color
                //else reset it to zero
                catSkill.color = Color.grey;
            }
        }
        else if (player == PlayerController.Player.P2)
        {
            if (type == "Basic")
            {
                chickenBasic.fillAmount = value / maxcd;
                //if the cooldown is still ongoing, shade the color
                //else reset it to zero
                chickenBasic.color = Color.grey;
            }
            else if (type == "Skill")
            {
                chickenSkill.fillAmount = value / maxcd;
                //if the cooldown is still ongoing, shade the color
                //else reset it to zero
                chickenSkill.color = Color.grey;
            }
        }

    }

    //reset the shading when the cooldown is up
    public void ResetShade(string type, PlayerController.Player player)
    {
        if (player == PlayerController.Player.P1)
        {
            if (type == "Basic")
            {
                catBasic.color = Color.white;
                catBasic.fillAmount = 1;
            }
            else if (type == "Skill")
            {
                catSkill.color = Color.white;
                catSkill.fillAmount = 1;
            }
        }
        else if (player == PlayerController.Player.P2)
        {
            if (type == "Basic")
            {
                chickenBasic.color = Color.white;
                chickenBasic.fillAmount = 1;
            }
            else if (type == "Skill")
            {
                chickenSkill.color = Color.white;
                chickenSkill.fillAmount = 1;
            }
        }
    }

    //check on shade status
    public Color CheckShade(string type, PlayerController.Player player)
    {
        if (player == PlayerController.Player.P1)
        {
            if (type == "Basic")
            {
                return catBasic.color;
            }
            else if (type == "Skill")
            {
                return catSkill.color;
            }
        }
        else if (player == PlayerController.Player.P2)
        {
            if (type == "Basic")
            {
                return chickenBasic.color;
            }
            else if (type == "Skill")
            {
                return chickenSkill.color;
            }
        }
        return Color.black;
    }

    //Show its buffed
    public void BuffShade()
    {
        catSkill.color = buffedColor;
    }
}
