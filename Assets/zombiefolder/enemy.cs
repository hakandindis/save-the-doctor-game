using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private Animator zombieanim;
    private CharacterController Karakter;

    public Transform hedef;
    private NavMeshAgent Agent;
    public float mesafe;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Karakter = GetComponent<CharacterController>();

        zombieanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //mesafe = Vector3.Distance(Transform.position, hedef.position);
        mesafe = Vector3.Distance(transform.position, hedef.position);
        Agent.destination = hedef.position;

        if (mesafe <= 1)
        {
            // enable hitting animation
        }
    }
}
