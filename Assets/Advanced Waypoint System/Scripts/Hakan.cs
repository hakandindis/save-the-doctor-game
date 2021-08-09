using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hakan : MonoBehaviour
{
    public float health;
    public GameObject canvas;
    private Rigidbody rb;
    public GameObject time;
    public GameObject finish;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        time.GetComponent<NewTimer>().currentTime = 30f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (health <= 0 || time.GetComponent<NewTimer>().isFinish() )
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            StopGame(canvas);
        }
        if (finish.GetComponent<GameOver>().finish_check)
        {
            Debug.Log("finish deneme");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("LOOG");
            other.gameObject.transform.parent.GetComponent<ZombieVur>().player_check = gameObject;
            other.gameObject.transform.parent.GetComponent<ZombieVur>().player_check = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "attack" && !other.GetComponentInParent<ZombieManager>().death_check)
        {
            Debug.Log("HSRET");
            other.GetComponentInParent<ZombieManager>().attack_check = true;
            other.GetComponentInParent<ZombieManager>().zombie_state = ZombieManager.Zombie_State.Attack;
            //StartCoroutine(TakeDamage());
            health -= Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "attack")
        {
            other.GetComponentInParent<ZombieManager>().attack_check = false;
            other.GetComponentInParent<ZombieManager>().zombie_state = ZombieManager.Zombie_State.Run;

            //health--;
        }
    }
    public void StopGame(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0f;
    }
    IEnumerator TakeDamage()
    {
        WaitForSeconds wait = new WaitForSeconds(10f);
        Debug.Log("LOOLLLLLL");
        health--;
        yield return wait;
    }
}
