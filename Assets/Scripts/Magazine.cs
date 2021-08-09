using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Magazine : MonoBehaviour
{
    public GameObject player;
    public Image bulletImg;
    public TMP_Text bulletText;
    public int maxBullet = 30;
    public int currentBullet;


    // Start is called before the first frame update
    void Start()
    {
        currentBullet = player.GetComponent<FireSystem>().ammo;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentBullet = player.GetComponent<FireSystem>().ammo;

        if (isFinish(currentBullet))
        {
            bulletImg.fillAmount = 0f;
            bulletText.text = "0";
        }
        else
        {
            bulletImg.fillAmount = (float)currentBullet / maxBullet;
            bulletText.text = currentBullet.ToString();     
     
        }

    }

    public bool isFinish(int currentBullet)
    {
        

        if (currentBullet ==0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
    }


}
