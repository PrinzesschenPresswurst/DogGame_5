using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class Question_SO : ScriptableObject
{
    [SerializeField][TextArea(5,20)] string question = "Enter new text here";
    [SerializeField] private string[] answers = new string [4];
    [SerializeField] int correctAnswer;

    public string GetQuestionText()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswer;
    }

    public string GetAnswerTextByIndex(int index)
    {
        return answers[index];
    }

    public int GetAnswerAmount()
    {
        return answers.Length;
    }
}
