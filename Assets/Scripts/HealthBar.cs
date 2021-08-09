using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public TMP_Text text;


    private void Update()
    {
        UpdateText(player);
    }


    public void UpdateText(GameObject obj)
    {
        float value=obj.GetComponent<Hakan>().health;
        int x = (int)value;
        text.text = x.ToString();

    }
}
