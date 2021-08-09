using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
 public void PlayGame()
 {
  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
 }
/*
  public void Gotosettingsmenu()
  {
   SceneManager.LoadScene("settingsmenu");
  }

 public void Gotomainmenu()
 {
  SceneManager.LoadScene("Mainmenu");
 } */
 public void Quitgame()
 {
  Debug.Log("quit");
  Application.Quit();
 }
}
