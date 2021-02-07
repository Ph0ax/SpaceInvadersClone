using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{

    public void ButtonOnClickScoreboard()
    {
        MenuSwitch.ScoreboardTrigger = true;
        MenuSwitch.MenuTrigger = false;
    }
    public void ButtonOnClickMenu()
    {
        MenuSwitch.ScoreboardTrigger = false;
        MenuSwitch.MenuTrigger = true;
    }
}