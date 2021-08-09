using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelloader : MonoBehaviour
{
    public GameObject loadingscreen;
    public Slider slider;
    public Text progresstext;
    
    public void loadlevel(int sceneindex)
    {
        StartCoroutine(Loadasynchronously(sceneindex));
    }

    IEnumerator Loadasynchronously(int sceneindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);
  
        loadingscreen.SetActive(true);
            
         
        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress/.9f);
            slider.value = progress;
            progresstext.text = progress * 100f + "%";
            yield return null;

        }
    }
}

