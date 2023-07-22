using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header ("Questions")]
    [SerializeField] private List<Question_SO> questionSOList;
    private Question_SO currentQuestionSO;
    [SerializeField] private TextMeshProUGUI questionText;
    private int questionIndex;

    [Header ("Answers")]
    [SerializeField] private GameObject[] answerButtonArray;
    [SerializeField] private List<Button> validButtonList;
    private TextMeshProUGUI answerButtonText;
    private int correctAnswerIndex;
    
    [Header ("Button Colors")]
    [SerializeField] private Color correctAnswerColor;
    [SerializeField] private Color wrongAnswerColor;
    [SerializeField] private Color defaultColor;
    
    [Header ("Audio")]
    [SerializeField] private AudioClip rightClip;
    [SerializeField] private AudioClip wrongClip;
    private AudioSource audioSource;

    [Header("Score")] 
    private ScoreKeeper scoreKeeper;

    [Header("Slider")] 
    [SerializeField] Slider progressBar;
    

    private void Start()
    {
        questionIndex = 0;
        GetNextQuestion();
        audioSource = GetComponent<AudioSource>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questionSOList.Count+1;
        progressBar.value = 1;
    }
    
    public void GetNextQuestion()
    {
        if (questionSOList.Count == 0)
        {   
            FindObjectOfType<GameManager>().DisplayEndScreen();
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
            progressBar.value++;
        }
    }
    
    void PickQuestionSO()
    {
        int randomIndex = Random.Range(0, questionSOList.Count);
        currentQuestionSO = questionSOList[randomIndex];
        questionSOList.Remove(currentQuestionSO);
    }

    private void GenerateQuestionAndAnswer()
    {
        questionText.text = currentQuestionSO.GetQuestionText();
        correctAnswerIndex = currentQuestionSO.GetCorrectAnswerIndex();
        
        int answerCount = currentQuestionSO.GetAnswerAmount();
        int buttonCount = answerButtonArray.Length;

        for (int i = 0; i < buttonCount; i++)
        {
            if (i < answerCount)
            {
                validButtonList.Add(answerButtonArray[i].GetComponentInChildren<Button>());
                answerButtonText = validButtonList[i].GetComponentInChildren<TextMeshProUGUI>();
                answerButtonText.text = currentQuestionSO.GetAnswerTextByIndex(i);
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
            scoreKeeper.IncreaseCorrectAnswers();
            audioSource.PlayOneShot(rightClip);
        }
        
        else if (index != correctAnswerIndex)
        {
            ShowRightAnswer();
            questionText.text = "Nope! \nThe right answer was: \n " + currentQuestionSO.GetAnswerTextByIndex(correctAnswerIndex);
            currentButtonImage.color = wrongAnswerColor;
            scoreKeeper.IncreaseWrongAnswers();
            audioSource.PlayOneShot(wrongClip);
        }
    }

    public void TimeHasRunOutHandler()
    {
        SetButtonState(false);
        ShowRightAnswer();
        questionText.text = "Too Slow! \nThe right answer was: \n " + currentQuestionSO.GetAnswerTextByIndex(correctAnswerIndex);
        scoreKeeper.IncreaseWrongAnswers();
        audioSource.PlayOneShot(wrongClip);
    }

    private void ShowRightAnswer()
    {
        Image rightAnswerButtonImage = answerButtonArray[correctAnswerIndex].GetComponent<Button>().image;
        rightAnswerButtonImage.color = Color.yellow;
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
