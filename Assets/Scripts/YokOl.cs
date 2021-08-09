using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokOl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] toplar = GameObject.FindGameObjectsWithTag("top");
        foreach (GameObject top in toplar)
        {
            top.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
