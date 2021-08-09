using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Worq;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navmeshagent;
    public bool check = false;
    void Start()
    {
        navmeshagent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            Follow();
        }
        
    }
    void Follow()
    {
        //gameObject.GetComponent<AWSPatrol>().interruptPatrol = true;
        navmeshagent.SetDestination(player.transform.position);
    }
}
