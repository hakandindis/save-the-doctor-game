using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieVur : MonoBehaviour
{
    public bool player_check;
    private NavMeshAgent navMeshAgent;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mesafe = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if(mesafe <= 10)
        {
            navMeshAgent.SetDestination(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z));
        }
        if (player_check && !gameObject.GetComponent<ZombieManager>().death_check)
        {
            //Debug.Log("LOG2");
            //Debug.Log(navMeshAgent.gameObject.name);
            //Debug.Log(player.transform.position.x);
            navMeshAgent.SetDestination(new Vector3(player.transform.position.x,gameObject.transform.position.y,player.transform.position.z));
        }
    }
   
}
