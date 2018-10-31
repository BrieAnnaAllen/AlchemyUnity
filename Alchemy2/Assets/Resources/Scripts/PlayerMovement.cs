using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private float speed;
    private float jumpspeed;
    private float fallmultipler;
    private float lowjumpmultipler;
    private bool onGround = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lowjumpmultipler = 3;
        fallmultipler = 6;
        jumpspeed = 50;
    }
    
    void FixedUpdate()
    {

        Movement();
    }
    private void CalculateCharacterRotation()
    {
        float turnSpeed = 0.1f;
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        if(movementDirection.magnitude > turnSpeed)
        {
            Quaternion newRotation = Quaternion.LookRotation(movementDirection);
            newRotation.SetLookRotation(Camera.main.transform.forward, Camera.main.transform.up);
            newRotation.eulerAngles = new Vector3(0, newRotation.eulerAngles.y, 0);
            rb.transform.rotation = newRotation;
        }
    }
    private void Movement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float moveHorizontal = horizontalInput * speed;
        float moveVertical = verticalInput * speed;
        float jump = Time.deltaTime * jumpspeed;
        Vector3 movement = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
       /* if (Input.GetButtonDown("Jump") && onGround)
         {
             rb.velocity = Vector3.up * jumpspeed;
             onGround = false;
         }*/
         Vector3 moveThisObject = (cameraForward * verticalInput + cameraRight * horizontalInput) * speed;
         moveThisObject.y = rb.velocity.y;
         rb.velocity = moveThisObject;
         CalculateCharacterRotation();
        
        /*
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallmultipler - 1) * Time.deltaTime;
        }
        else if (!Input.GetButtonDown("Jump") && (rb.velocity.y > 0))
        {

            rb.velocity += Vector3.up * Physics.gravity.y * (lowjumpmultipler - 1) * Time.deltaTime;

        }*/
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onGround = true;
        }
    }



}
