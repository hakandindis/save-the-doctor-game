using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 2f;
    [SerializeField] private Rigidbody rocketRb;

    private Rigidbody _RocketRb;



    // Start is called before the first frame update
    void Start()
    {
        if (rocketRb == null)
        {
            _RocketRb = this.gameObject.GetComponent<Rigidbody>();
        }
        else
        {
            _RocketRb = rocketRb;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }

        if (Input.GetKey(KeyCode.W))
        {
            Walk();
            //RocketActive();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Back();
            //RocketActive();
        }

    }



    private void TurnRight()
    {
        //transform.Rotate(Vector3.right * (Time.deltaTime * rotateSpeed));
        _RocketRb.AddRelativeForce(Vector3.right * (speed * Time.deltaTime));
    }

    private void TurnLeft()
    {
        //transform.Rotate(Vector3.left * (Time.deltaTime * rotateSpeed));
        _RocketRb.AddRelativeForce(Vector3.left * (speed * Time.deltaTime));
    }


    private void Walk()
    {
        _RocketRb.AddRelativeForce(Vector3.forward * (speed * Time.deltaTime));

    }

    private void Back()
    {
        _RocketRb.AddRelativeForce(Vector3.back * (speed * Time.deltaTime));

    }
}
