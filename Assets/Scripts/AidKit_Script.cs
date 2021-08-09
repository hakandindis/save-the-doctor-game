using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit_Script : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    


    private void OnTriggerStay(Collider other)
    {
        
        Debug.Log("xxxx");
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                AidKit(player);
                Destroy(this.gameObject);
            }
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
    


    public void AidKit(GameObject obj)
    {
        obj.GetComponent<InventoryList>().numbers[1] += 1;
    }
}
