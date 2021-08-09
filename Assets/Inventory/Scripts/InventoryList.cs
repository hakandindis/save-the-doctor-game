using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryList : MonoBehaviour
{
    //[SerializeField] private GameObject[] masks; // to enable and disable object
    //[SerializeField] private GameObject[] syringes; // to enable and disable object
    //[SerializeField] private GameObject[] aidKits; // to enable and disable object
    //[SerializeField] private Image[] slots; // keys,aidkits,masks
    //[SerializeField] private GameObject inventory; // inventory panel

    public TMP_Text[] texts; // keys,aidkits,masks
    public int[] numbers; // keys,aidkits,masks

    public GameObject timeBar;
    public GameObject health;
    //public bool isNewScene = false;

    private void FixedUpdate()
    {
        //numbers[0]= 0;
        //numbers[1] = 0;
        //numbers[2] = 0;
        texts[0].text = numbers[0].ToString();
        texts[1].text = numbers[1].ToString();
        texts[2].text = numbers[2].ToString();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseKey();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            UseAidKit(health);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            UseMask(timeBar);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            numbers[0] += 1;
            texts[0].text = numbers[0].ToString();
            //Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Aidkit"))
        {
            numbers[1] += 1;
            texts[1].text = numbers[1].ToString();
            //Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Mask"))
        {
            numbers[2] += 1;
            texts[2].text = numbers[2].ToString();
            //Destroy(other.gameObject);
        }

        //if(other.gameObject.CompareTag("New Scene"))
        //{
        //    isNewScene = true;
        //}
    }

    */

    public void UseKey()
    {
        if (numbers[0]>0 )
        {
            
        }
    }

    public void UseAidKit(GameObject obj)
    {
        if (numbers[1] > 0)
        {
            obj.GetComponent<Hakan>().health +=5;
            numbers[1] -= 1;
            texts[1].text = numbers[1].ToString();
        }
        
    }

    public void UseMask(GameObject obj)
    {
        if (numbers[2] > 0)
        {
            obj.GetComponent<NewTimer>().currentTime += 15;
            obj.GetComponent<NewTimer>().gameTime += 15;
            numbers[2] -= 1;
            texts[2].text = numbers[2].ToString();
        }
    }
}
