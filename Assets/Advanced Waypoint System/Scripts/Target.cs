
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float health;
    public GameObject zombie;
    public GameObject key;
    private bool once_check;

    private void Start()
    {
        once_check = false;
        zombie = gameObject;
    }
    public void TakeDamage(float amount)
    {
        Debug.Log(amount);
        health = health - amount;
        Debug.Log("Kanyon Vadisi");
        if(health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        zombie.GetComponent<ZombieManager>().zombie_state = ZombieManager.Zombie_State.Die;
        GetComponent<NavMeshAgent>().Stop();
        if (!once_check)
        {
            Instantiate(key, gameObject.transform.position, Quaternion.identity);
            once_check = true;
        }
        Destroy(gameObject,3.5f);
    }
}
