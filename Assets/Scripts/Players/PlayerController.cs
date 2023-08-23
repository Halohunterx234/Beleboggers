using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity
{
    CharacterController controller;

    public float moveSpeed = 5f, rotationSpeed = 180f;
    Vector3 velocity;

    //Animation bools
    

    //Basic atk & skill
    public Abilities ability;

    //damage boost
    public bool doubleDmg;

    //UI icon
    public Image dmgBoostp1, dmgBoostp2;
    private Color trans = Color.white;

    public enum Player { P1, P2 };
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (!isLocked) healthBarUI.UpdateHP(hp, this);
        doubleDmg = false;
        trans.a = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //Gravity
        //Movement vector
        if (controller.isGrounded) velocity = Vector3.zero;
        else velocity += Physics.gravity * Time.deltaTime;
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal " + player.ToString()),
            0,
            Input.GetAxis("Vertical " + player.ToString()));

        //Movement Input
        Vector3 displacement = transform.TransformDirection(movement.normalized) * moveSpeed;

        if (displacement != Vector3.zero)
        {
            anim.SetBool("IsMoving", true);
            
        }
        else anim.SetBool("IsMoving", false);
        controller.Move((displacement + velocity) * Time.deltaTime);
        
        //anim.SetFloat("MoveX", movement.x);
        //anim.SetFloat("MoveY", movement.z);

        //If fire1 is held down
        if (Input.GetButtonDown("Fire1 " + player.ToString()))
        {
            //Activiate corresponding basic atk
            ability.BasicAttack();
        }
        //if fire1 is held down for controllers
        else if (Input.GetKeyDown(KeyCode.JoystickButton7) && player == Player.P2)
        {
            ability.BasicAttack();
        }

        //if fire2 is held down
        else if (Input.GetButtonDown("Fire2 " + player.ToString()))
        {
            //Activate skill
            ability.Skill1();
        }
        //if for controllers
        else if (Input.GetKeyDown(KeyCode.JoystickButton5) && player == Player.P2)
        {
            ability.Skill1();
        }
        //Rotate to input
        transform.Rotate(0, Input.GetAxis("Mouse X " + player.ToString()) * rotationSpeed * Time.deltaTime, 0);
    }

    //damage boost
    public IEnumerator dmgboost()
    {
        //activate the bool
        doubleDmg = true;

        //update the icon to show its dealing dmg
        if (player == Player.P2)
        {
            dmgBoostp2.color = Color.white;
        }
        else if (player == Player.P1)
        {
            dmgBoostp1.color = Color.white;
        }
        yield return new WaitForSeconds(30);
        doubleDmg = false;


        //update the icon to show its gone
        if (player == Player.P2)
        {
            dmgBoostp2.color = trans;
        }
        else if (player == Player.P1)
        {
            dmgBoostp1.color = trans;
        }
    }

    public void Tp(Vector3 destination)
    {
        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = destination;
        cc.enabled = true;
    }
}