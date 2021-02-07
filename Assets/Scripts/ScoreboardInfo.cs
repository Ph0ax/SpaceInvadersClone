using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardInfo : MonoBehaviour
{
    public int flag;
    public int ranked;
    public int CheckFlag;
   
    private Text scoreText;


    void Start()
    {
        flag = -1;
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        CheckFlag = flag;

        if (flag == 1)
        {
            scoreText.text = "Congratulations! You are " + ranked + " in scoreboard";
            scoreText.color = Color.green;
        }
        
        if (flag == 2)
        {
            scoreText.color = Color.red;
            scoreText.text = "Im sorry, but you did not managed to get onto the scoreboard :(";
        }

        
        
    }
}
