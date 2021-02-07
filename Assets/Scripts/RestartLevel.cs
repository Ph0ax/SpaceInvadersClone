using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{

    public void RestartOnClick()
    {
            PlayerScore.playerScore = 0;
            UIGameControl.isPlayerDead = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("Scene_001");
    }
    public void MenuOnClick()
    {
        PlayerScore.playerScore = 0;
        UIGameControl.isPlayerDead = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene_Menu");
    }
}
