using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;

    public int ranked;

    private int numbers;
    void Awake()
    {
        
        var json = PlayerPrefs.GetString("scores", "{}");
        sd = JsonUtility.FromJson<ScoreData>(json);

        numbers = sd.scores.Count();

        
        if (sd == null)
        {
            sd = new ScoreData();
        }
    }

    public IEnumerable<Score> GetHighScores()
    {
        return sd.scores.OrderByDescending(x => x.score);
            
    }
    public void AddScore(Score score)
    {
        if (numbers < 10)
        {
            sd.scores.Add(score);
            for (int i = 0; i < numbers; i++) 
            {
                if(sd.scores[i].score == score.score)
                {
                    ranked = i+1;
                }
            }
        }
        else ChangeScore(score);

    }

    private void OnDestroy()
    {
        SaveScore();
    }


    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        Debug.Log(json);
        PlayerPrefs.SetString("scores", json);
    }

    public void ChangeScore(Score score)
    {

        
        for (int i = 0; i <numbers-1; i++)
        { 
            if(sd.scores[i].score < score.score)
            {
                sd.scores[i].name = score.name;
                sd.scores[i].score = score.score;
                ranked = i+1;
                return;
            }
        }
        ranked = -1;
         
    }

}