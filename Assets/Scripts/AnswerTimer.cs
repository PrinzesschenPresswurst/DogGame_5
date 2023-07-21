using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerTimer : MonoBehaviour
{
    [SerializeField] float answerTime = 30.0f;
    [SerializeField] private Button nextQuestionButton;
    private float timerValue;
    private Image dogClock;
    public bool isAnsweringQuestion;

    private void Start()
    {
        nextQuestionButton.interactable = false;
        ResetTimer();
    }
    
    void Update()
    {
        if (isAnsweringQuestion == true)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        if (timerValue > 0)
        {
            timerValue = timerValue - Time.deltaTime;
            dogClock.fillAmount -= 1f / answerTime* Time.deltaTime;
        }

        else if (timerValue <= 0)
        {
            StopTimer();
            FindObjectOfType<Quiz>().TimeHasRunOutHandler();
        }
    }

    public void StopTimer()
    {
        isAnsweringQuestion = false;
        nextQuestionButton.interactable = true;
    }

    public void ResetTimer()
    {
        dogClock = GetComponent<Image>();
        dogClock.fillAmount = 1f;
        isAnsweringQuestion = true;
        timerValue = answerTime;
        nextQuestionButton.interactable = false;
    }
}
