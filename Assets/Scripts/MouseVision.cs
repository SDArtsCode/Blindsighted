using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVision : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;


    float rotateX = 0f;
    public Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
         
        rotateX -= mouseY;
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);


        transform.localRotation = Quaternion.Euler(rotateX, 0f, 0f);


        playerBody.Rotate(Vector3.up * mouseX);
       
    }
}