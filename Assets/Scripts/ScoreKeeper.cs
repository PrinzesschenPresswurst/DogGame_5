using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int correctAnswers = 0;
    private int wrongAnswers = 0;
    private int questionsSeen = 0;
    private int score;
    private int endscore;
    [SerializeField] private TextMeshProUGUI endScoreText;
    

    public void IncreaseCorrectAnswers()
    {
        correctAnswers++;
        questionsSeen++;
        UpdateScore();
    }

    public void IncreaseWrongAnswers()
    {
        wrongAnswers++;
        questionsSeen++;
        UpdateScore();
    }

    public int GetScore()
    {
        return score * 100;
    }

    void UpdateScore()
    {
        score = correctAnswers - wrongAnswers;
        scoreText.text = "Score: " + score * 100;
        endScoreText.text = "Your final score is = "+ score * 100;
        if (score < 0)
        {
            scoreText.color = Color.red;
        }
        else if (score == 0)
        { 
            scoreText.color = Color.blue;
        }
        
        else if (score > 0)
        {
            scoreText.color = Color.green;
        }
    }
}
