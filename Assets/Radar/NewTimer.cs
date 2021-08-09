using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NewTimer : MonoBehaviour
{
    public Image timeImg;
    public GameObject player;
    public TMP_Text timeText;

    public float gameTime;
    public float currentTime=3;
    
    // Update is called once per frame
    void Update()
    {
        currentTime = gameTime - Time.time;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int secondes = Mathf.FloorToInt(currentTime - minutes * 60f);
        string textTime = string.Format("{0:0}:{1:00}", minutes, secondes);


        if (isFinish())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StopGame();
        }
        else
        {
            //timerText.text = textTime;
            timeText.text = textTime;
            timeImg.fillAmount = currentTime/gameTime;
        }

    }

    public bool isFinish()
    {
        float value = currentTime / gameTime;

        if(value <= 0.005)
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
        player.GetComponent<Hakan>().StopGame(player.GetComponent<Hakan>().canvas);
        Time.timeScale = 0f;

    }

}
