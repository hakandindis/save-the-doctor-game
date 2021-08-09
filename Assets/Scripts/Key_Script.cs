using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Script : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Destroy(this.gameObject);
                Key(player);
            }
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
    

    public void Key(GameObject obj)
    {
        obj.GetComponent<InventoryList>().numbers[0] += 1;
    }
}
