using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float answerTime = 30.0f;
    private Image clockImage;
    private float timerValue;
    [SerializeField] float changeFillPerSecond;
    private bool isAnsweringQuestion;
    private Image dogClock;

    private void Start()
    {
        timerValue = answerTime;
        dogClock = GetComponent<Image>();
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue = timerValue - Time.deltaTime;
        changeFillPerSecond = 1f / answerTime* Time.deltaTime;
        dogClock.fillAmount -= changeFillPerSecond;
        
        if (timerValue <= 0)
        {
            timerValue = answerTime;
            dogClock.fillAmount = 1f;
            isAnsweringQuestion = false;
        }
    }
}
