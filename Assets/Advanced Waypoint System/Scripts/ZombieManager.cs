using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }
    public Zombie_State zombie_state;
    public Animator zombieanimator;
    private Rigidbody rb;
    public bool attack_check;
    public bool death_check;
    
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        attack_check = false;
    }
    private void Awake()
    {
        //if(Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    private void Update()
    {
        
        switch (zombie_state)
        {
            case Zombie_State.Idle:
                Idle();
                break;
            case Zombie_State.Run:
                Run();
                break;
            case Zombie_State.Attack:
                Attack();
                break;
            case Zombie_State.Die:
                Die();
                break;
        }
        if (death_check)
        {
            gameObject.GetComponent<NavMeshAgent>().speed = 0;
            
            
            
        }
        if (attack_check)
        {
            zombie_state = Zombie_State.Attack;
            
        }
        if(rb.velocity.x != 0 || rb.velocity.y != 0 || rb.velocity.z != 0 && !attack_check)
        {
            zombie_state = Zombie_State.Run;
        }
        
        
    }
    public enum Zombie_State
    {
        Idle,
        Run,
        Attack,
        Die
    }
    public void Idle()
    {
        zombieanimator.ResetTrigger("Run");
        zombieanimator.ResetTrigger("Attack");
        zombieanimator.SetTrigger("Idle");
    }
    public void Run()
    {
        zombieanimator.ResetTrigger("Idle");
        zombieanimator.ResetTrigger("Attack");
        zombieanimator.SetTrigger("Run");
    }
    public void Attack()
    {
        zombieanimator.ResetTrigger("Run");
        zombieanimator.ResetTrigger("Idle");
        zombieanimator.SetTrigger("Attack");
    }
    public void Die()
    {
        death_check = true;
        zombieanimator.SetTrigger("Die");
    }
}

