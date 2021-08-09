using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_Next_Scene : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(player.GetComponent<InventoryList>().numbers[0] > 15)
                SceneManager.LoadScene(3);
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }

}
