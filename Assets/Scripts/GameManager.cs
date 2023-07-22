using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas gameScreenCanvas;
    [SerializeField] private Canvas endScreenCanvas;
    
    private void Start()
    {
        gameScreenCanvas.GetComponent<Canvas>().enabled = true;
        endScreenCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void DisplayEndScreen()
    {
        gameScreenCanvas.GetComponent<Canvas>().enabled = false;
        endScreenCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
