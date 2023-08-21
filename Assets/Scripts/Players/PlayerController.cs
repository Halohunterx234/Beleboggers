using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity
{
    CharacterController controller;
    Animator anim;
    public float moveSpeed = 5f, rotationSpeed = 180f;
    Vector3 velocity;
    //Basic atk & skill
    public Abilities ability;

    public enum Player { P1, P2 };
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
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
}