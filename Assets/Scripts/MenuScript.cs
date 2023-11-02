using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
  public void LoadDeductionScene()
  {
    SceneManager.LoadScene("PlayerDeductionScene");
  }
  public void LoadRetryScene()
  {
    SceneManager.LoadScene("RetryScene");
  }

  public void LoadGameOverScene()
  {
    SceneManager.LoadScene("SuccessScene");
  }

  public void LoadGameScene()
  {
    SceneManager.LoadScene("MainScene");
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
