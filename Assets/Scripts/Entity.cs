using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all entities that have hp
public class Entity : MonoBehaviour
{
    //essential stuff
    public int hp;

    //events
    public GameObject deathParticle;

    //references
    public List<SkinnedMeshRenderer> Meshes; //throw into the inspector the parts w materials

    private void Awake()
    {

    }
    //method to check/update hp
    public void UpdateHealth(int dmgvalue)
    {
        hp -= dmgvalue;
        if (Mathf.Sign(dmgvalue) == 1) UpdateColor(Color.red); //dmg color
        else if (Mathf.Sign(dmgvalue) == -1) UpdateColor(Color.green); //heal color
        if (hp <= 0)
        {
            DeathEvent();
        }
        else StartCoroutine(ResetColor());
    }

    //update color
    void UpdateColor(Color color)
    {
        print(color);
        foreach (SkinnedMeshRenderer skm in Meshes)
        {
            skm.material.color = color;
        }
    }
    //reset color after a while
    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(.5f);
        UpdateColor(Color.white);
    }
    //death event
    public void DeathEvent()
    {
      //death animations
      Destroy(this.gameObject);
    }

}
