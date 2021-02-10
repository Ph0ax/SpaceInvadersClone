using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIGameControl : MonoBehaviour
{

    public static bool isPlayerDead;
    public static bool win;
    public static bool scoreboardInput;



    public GameObject gameOver;
    public GameObject ScoreboardInput;
    public GameObject Controls;
    public GameObject InputField;

    public ScoreManager scoreManager;

    public ScoreboardInfo scoreboard;

    private string PlayerName;
    private string info;
    private bool BeenSent;
    



    public void ScoreboardSender()
    {
        if(BeenSent == false)
        {

            if (InputField.GetComponent<Text>().text == "")
            {
                PlayerName = "No name";
            }
            else
            {
                PlayerName = InputField.GetComponent<Text>().text;
            }

            scoreManager.AddScore(new Score(PlayerName, PlayerScore.playerScore));

            /*
            if (scoreManager.ranked != -1)
            {

                scoreboard.ranked = scoreManager.ranked;
                scoreboard.flag = 1;
            }
            else
            {
                scoreboard.flag = 2;
            }
            */

            //Branchless conversion=====================================================
            var scoremanagerif = Convert.ToInt16(scoreManager.ranked != -1);
            scoreboard.ranked = scoreManager.ranked * scoremanagerif;
            scoreboard.flag = (1 * scoremanagerif) + (2 * Convert.ToInt16(!(scoreManager.ranked != -1)));

            //==========================================================================

            BeenSent = true;
        }
        

    }

    void Start()
    {
        isPlayerDead = false;
        win = false;
        scoreboardInput = false;
        Time.timeScale = 1;
        BeenSent = false;

        Controls.SetActive(true);
        gameOver.SetActive(false);
        ScoreboardInput.SetActive(false);
    }
    void Update()
    {
        if (isPlayerDead == true)
        {
            Controls.SetActive(false);
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        if (win == true)
        {
            Controls.SetActive(false);
            if (ScoreboardInput.activeSelf == false)
            {
                ScoreboardInput.SetActive(true);
            } 
            Time.timeScale = 0;
        }
    }
}
