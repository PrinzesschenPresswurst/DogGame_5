using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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
    [SerializeField] private TextMeshProUGUI scoreText;
    private AudioSource audioSource;
    [SerializeField] private AudioClip rightClip;
    [SerializeField] private AudioClip wrongClip;
    private int score;

    private void Start()
    {
        questionIndex = 0;
        GetNextQuestion();
        scoreText.text = "Score:" + score;
        audioSource = GetComponent<AudioSource>();
    }
    
    public void GetNextQuestion()
    {
        
        if (questionIndex == questionSOList.Count)
        {
            SceneManager.LoadScene("EndScreen");
        }

        else
        {
            validButtonList.Clear();
            EnableArrayButtons();
            PickQuestionSO();
            GenerateQuestionAndAnswer();
            SetButtonState(true);
            SetDefaultButtonColors();
            FindObjectOfType<AnswerTimer>().ResetTimer();
        }
    }
    
    void PickQuestionSO()
    {
        if (questionIndex == questionSOList.Count)
        {
            questionIndex = 0;
        }
        questionSO = questionSOList[questionIndex];
    }

    private void GenerateQuestionAndAnswer()
    {
        questionText.text = questionSO.GetQuestionText();
        correctAnswerIndex = questionSO.GetCorrectAnswerIndex();
        
        int answerCount = questionSO.GetAnswerAmount();
        int buttonCount = answerButtonArray.Length;

        for (int i = 0; i < buttonCount; i++)
        {
            if (i < answerCount)
            {
                validButtonList.Add(answerButtonArray[i].GetComponentInChildren<Button>());
                answerButtonText = validButtonList[i].GetComponentInChildren<TextMeshProUGUI>();
                answerButtonText.text = questionSO.GetAnswerTextByIndex(i);
            }
            
            else if (i >= answerCount)
            {
                answerButtonArray[i].SetActive(false);
            }
        }
    }

    private void OnAnswerSelect(int index)
    {
        FindObjectOfType<AnswerTimer>().StopTimer();
        SetButtonState(false);
        Image currentButtonImage = answerButtonArray[index].GetComponent<Button>().image;
        
        if (index == correctAnswerIndex)
        {
            questionText.text = "right";
            currentButtonImage.color = correctAnswerColor;
            score++;
            UpdateScore();
            audioSource.PlayOneShot(rightClip);
        }
        else if (index != correctAnswerIndex)
        {
            ShowRightAnswer();
            questionText.text = "Nope! \nThe right answer was: \n " + questionSO.GetAnswerTextByIndex(correctAnswerIndex);
            currentButtonImage.color = wrongAnswerColor;
            score--;
            UpdateScore();
            audioSource.PlayOneShot(wrongClip);
        }
        
        questionIndex ++;
        
    }
    
    public void TimeHasRunOutHandler()
    {
        SetButtonState(false);
        ShowRightAnswer();
        questionText.text = "Too Slow! \nThe right answer was: \n " + questionSO.GetAnswerTextByIndex(correctAnswerIndex);
        score--;
        UpdateScore();
        audioSource.PlayOneShot(wrongClip);
        
        questionIndex ++;
    }

    private void ShowRightAnswer()
    {
        Image rightAnswerButtonImage = answerButtonArray[correctAnswerIndex].GetComponent<Button>().image;
        rightAnswerButtonImage.color = Color.yellow;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score:" + score;
        if (score < 0)
        {
            scoreText.color = Color.red;
        }
        else if (score >= 0)
        {
            scoreText.color = Color.blue;
        }
    }
    
    private void SetButtonState(bool state)
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

    private void EnableArrayButtons()
    {
        foreach (var answerButton in answerButtonArray)
        {
            answerButton.SetActive(true);
        }
    }
}
