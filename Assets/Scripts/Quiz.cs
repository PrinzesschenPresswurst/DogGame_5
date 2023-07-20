using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UIElements.Toggle;

public class Quiz : MonoBehaviour
{
    [SerializeField] private Question_SO questionSO;
    [SerializeField] private List<Question_SO> questionSOList;
    private int questionIndex;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private GameObject[] answerButtonArray;
    private TextMeshProUGUI answerButtonText;
    private int correctAnswerIndex;
    [SerializeField] private List<Button> validButtonList;
    [SerializeField] private Color correctAnswerColor;
    [SerializeField] private Color wrongAnswerColor;
    [SerializeField] private Color defaultColor;

    void Start()
    {
        GetNextQuestion();
        questionIndex = 0;
    }
    
    void GetNextQuestion()
    {
        validButtonList.Clear();
        EnableArrayButtons();
        PickQuestionSO();
        GenerateQuestionAndAnswer();
        SetButtonState(true);
        SetDefaultButtonColors();
    }

    void PickQuestionSO()
    {
        if (questionIndex == questionSOList.Count)
        {
            questionIndex = 0;
        }
        questionSO = questionSOList[questionIndex];
    }

    void GenerateQuestionAndAnswer()
    {
        questionText.text = questionSO.GetQuestionText();
        
        int answerCount = questionSO.GetAnswerAmount();
        int buttonCount = answerButtonArray.Length;

        for (int i = 0; i < buttonCount; i++)
        {
            if (i < answerCount)
            {
                validButtonList.Add(answerButtonArray[i].GetComponentInChildren<Button>());
                answerButtonText = validButtonList[i].GetComponentInChildren<TextMeshProUGUI>();
                answerButtonText.text = questionSO.GetAnswerTextByIndex(i);
                //validButtonList.Add(answerButtonArray[i].GetComponentInChildren<Button>());
            }
            
            else if (i >= answerCount)
            {
                answerButtonArray[i].SetActive(false);
                //Destroy(answerButtonArray[i]);
            }
        }
    }

    public void OnAnswerSelect(int index)
    {
        correctAnswerIndex = questionSO.GetCorrectAnswerIndex();
        Image currentButtonImage = answerButtonArray[index].GetComponent<Button>().image;
        Image rightAnswerButtonImage = answerButtonArray[correctAnswerIndex].GetComponent<Button>().image;
        string correctAnswerText = questionSO.GetAnswerTextByIndex(correctAnswerIndex);

        if (index == correctAnswerIndex)
        {
            questionText.text = "right";
            currentButtonImage.color = correctAnswerColor;
        }
        else if (index != correctAnswerIndex)
        {
            questionText.text = "Nope, the right answer was: \n " + correctAnswerText;
            currentButtonImage.color = wrongAnswerColor;
            rightAnswerButtonImage.color = Color.yellow;
        }

        SetButtonState(false);
        questionIndex ++;
        Invoke("GetNextQuestion",1f);
    }

    void SetButtonState(bool state)
    {
        foreach (var validButton in validButtonList)
        {
            validButton.GetComponent<Button>().interactable = state;
        }
    }

    private void SetDefaultButtonColors()
    {
        foreach (var validButton in validButtonList)
        {
            validButton.GetComponent<Button>().image.color = defaultColor;
        } 
    }

    void EnableArrayButtons()
    {
        foreach (var answerButton in answerButtonArray)
        {
            answerButton.SetActive(true);
        }
    }
}
