using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controlls;
    public float gravity = -9.81f;
    public float jumpHeight = 2.5f;
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    bool isGrounded;

    [SerializeField] float speed = 12f;

    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveLocalTransform = transform.right * x + transform.forward * z;

        controlls.Move(moveLocalTransform * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controlls.Move(velocity * Time.deltaTime);
    }
}
