using UnityEngine;
using UnityEngine.AI;
public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    bool check = false;
    private void Update()
    {
    if (check)
        {
            enemy.SetDestination(player.position);

        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            check = true;
        }
    }
   
}