using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject lostText;
    public GameObject winText;
    public GameObject instructionPanel;
    public GameObject instructionButton;

    private void Awake()
    {
        Instance = this;
    }


   public void ShowLostUI()
    {
        lostText.SetActive(true);
    }

    public void ShowWinUI()
    {
        winText.SetActive(true);
    }

    public void ShowInstruction()
    {
        instructionPanel.SetActive(true);
        instructionButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void ReturnToGameFromInstruction()
    {
        instructionPanel.SetActive(false);
        instructionButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
