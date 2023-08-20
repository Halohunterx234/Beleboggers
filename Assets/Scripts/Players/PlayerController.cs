using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator anim;
    public float moveSpeed = 5f, rotationSpeed = 180f;
    Vector3 velocity;

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
        if (controller.isGrounded) velocity = Vector3.zero;
        else velocity += Physics.gravity * Time.deltaTime;
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal " + player.ToString()),
            0,
            Input.GetAxis("Vertical " + player.ToString()));

        if (Player.P2 == player) print(movement);
        Vector3 displacement = transform.TransformDirection(movement.normalized) * moveSpeed;
        controller.Move((displacement + velocity) * Time.deltaTime);
        //anim.SetFloat("MoveX", movement.x);
        //anim.SetFloat("MoveY", movement.z);

        transform.Rotate(0, Input.GetAxis("Mouse X " + player.ToString()) * rotationSpeed * Time.deltaTime, 0);
    }
}