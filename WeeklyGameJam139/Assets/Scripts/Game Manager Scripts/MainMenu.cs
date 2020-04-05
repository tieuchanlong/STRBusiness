using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainScreen;
    [SerializeField] private GameObject HowScreen;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene(2);
    }

    public void HowTo()
    {
        MainScreen.SetActive(false);
        HowScreen.SetActive(true);
    }

    public void Back()
    {
        MainScreen.SetActive(true);
        HowScreen.SetActive(false);
    }
}
