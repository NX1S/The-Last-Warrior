using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider coll;
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float jumpForce = 4f;
    private float dirx;
    private float dirz;
    public float moveSpeedOld;
    private bool isRunning = false;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        moveSpeedOld = moveSpeed;
    }

    void Update()
    {
        
        dirx = Input.GetAxis("Horizontal");
        dirz = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(dirx * moveSpeed, rb.velocity.y, dirz * moveSpeed);
        //transform.Translate(rb.velocity, Space.World);
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isRunning)
        {
            isRunning = true;
            moveSpeed = moveSpeedOld + 2;
            Debug.Log("run");
        } else if (Input.GetKeyUp(KeyCode.LeftShift) && isRunning)
        {
            isRunning = false;
            moveSpeed = moveSpeedOld;
            Debug.Log("not running");
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }
        }
    }

    void FixedUpdate()
    {
        //If it works dont touch it.
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float cameraRot = Camera.main.transform.rotation.eulerAngles.y;
        rb.position += Quaternion.Euler(0, cameraRot, 0) * input * moveSpeed * Time.deltaTime;
        //rotate the player towards the camera

        //transform.localEulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        //if (rb.velocity.z > 3) {}
        
        /*Vector3 forward = Camera.main.transform.forward;  
        forward.y = 0;
        forward = forward.normalized;
        this.transform.Translate(forward, Space.World);*/
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
