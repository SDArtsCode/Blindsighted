using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controlls;
    public float gravity = -9.81f;
    public float jumpHeight = 2.5f;
    public Transform groundCheck;
    public float groundRadius = 0.75f;
    public LayerMask groundMask;
    bool isGrounded;

    bool pulsing = false;
    [SerializeField] Light playerLight;
    [SerializeField] float lerpUpSpeed;
    [SerializeField] float lerpDownSpeed;
    [SerializeField] float maxRange;
    [SerializeField] float minRange;
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

        if (Input.GetMouseButtonDown(0))
        {
            pulsing = true;
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

        if (pulsing)
        {
            Pulse();
        }
    }

    void Pulse()
    {
        bool switched = false;
        float range = playerLight.range;
        if (maxRange - range >= 0.1 && !switched)
        {
            range = Mathf.Lerp(range, maxRange, lerpUpSpeed);
            switched = true;
        }
        else if(range - minRange >= 0.1 && switched)
        {
            range = Mathf.Lerp(range, minRange, lerpDownSpeed);
        }
        else
        {
            range = minRange;
            pulsing = false;
        }
        playerLight.range = range;
    }
}
