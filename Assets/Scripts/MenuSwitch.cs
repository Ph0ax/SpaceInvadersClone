using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Scoreboard;
    public static bool ScoreboardTrigger = false;
    public static bool MenuTrigger = true;

    // Update is called once per frame

    void Update()
    {
        if (ScoreboardTrigger == true)
        {
            Menu.SetActive(false);
            Scoreboard.SetActive(true);
        }
        if(MenuTrigger == true)
        {
            Scoreboard.SetActive(false);
            Menu.SetActive(true);
        }
    }

}
