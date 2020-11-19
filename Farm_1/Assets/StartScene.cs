using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
  public void SceneLoader()
  {
    if (PlayerPrefs.GetInt("FirstLaunch") == 0)
    {
      //First launch
      PlayerPrefs.SetInt("FirstLaunch", 1);
      SceneManager.LoadScene(1);
    }
    else
    {
      //Load scene_02 if its not the first launch
      SceneManager.LoadScene(2);
    }
  }
}
