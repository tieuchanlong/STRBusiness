using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManage : MonoBehaviour
{
    private float cur_time = 1;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private Button PauseButton;
    [SerializeField] private GameObject CoinPan;

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        cur_time = Time.timeScale;
        Time.timeScale = 0;
        PauseButton.gameObject.SetActive(false);
        CoinPan.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = cur_time;
        PauseButton.gameObject.SetActive(true);
        CoinPan.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void ExitMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
