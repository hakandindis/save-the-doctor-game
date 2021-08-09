using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float jump_force;
    [SerializeField] bool lockCursor = true;
    //[SerializeField] CharacterController controller;
    Rigidbody rb;
    float cameraPitch = 0.0f;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    bool groundcheck;

    void Start()
    {
        groundcheck = true;
        rb = GetComponent<Rigidbody>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        
        
    }
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.W)|| Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0,0,walkSpeed * Time.fixedDeltaTime));
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, walkSpeed * -1 * Time.fixedDeltaTime));
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(walkSpeed * -1 * Time.fixedDeltaTime, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(walkSpeed * Time.fixedDeltaTime, 0, 0));
        }
        if(Input.GetKeyDown(KeyCode.Space) && groundcheck)
        {
            Jump();
        }
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jump_force, 0));
        groundcheck = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        groundcheck = true;
        Debug.Log("Log");
    }

}
