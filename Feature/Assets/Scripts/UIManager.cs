using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject lostText;
    public GameObject winText;

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
}
