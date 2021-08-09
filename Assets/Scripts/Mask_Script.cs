using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask_Script : MonoBehaviour
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
                Mask(player);
                Destroy(this.gameObject);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }

    public void Mask(GameObject obj)
    {
        obj.GetComponent<InventoryList>().numbers[2] += 1;
    }

}
