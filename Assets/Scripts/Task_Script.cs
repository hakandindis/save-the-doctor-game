using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Script : MonoBehaviour
{
    public GameObject taskScreen;
    public bool isEnableTaskScreen;

    private void Start()
    {
        isEnableTaskScreen = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("xxx");
            isEnableTaskScreen = !isEnableTaskScreen;
        }

        if (isEnableTaskScreen)
        {
            taskScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            taskScreen.SetActive(false);
            Time.timeScale = 1f;
        }

    }
}
