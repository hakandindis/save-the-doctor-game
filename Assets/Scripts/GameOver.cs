using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public bool finish_check = false;
    //button function
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);

    }


    public void xxxx()
    {
        SceneManager.LoadScene(2);
    }
    public void GoMainScene()
    {
        finish_check = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }
}
