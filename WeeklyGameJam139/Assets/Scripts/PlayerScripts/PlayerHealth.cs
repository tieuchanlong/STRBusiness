using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject CoinsPanel;
    [SerializeField] private AudioSource DieSound;
    [SerializeField] private AudioSource Background;
    [SerializeField] private AudioSource Traffic;
    [SerializeField] private GameObject PauseButton;

    public int Health = 100;

    public bool Dead
    {
        get
        {
            return Health <= 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            //Health = 1;
            StartCoroutine(Dying());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<EnemyControl>().Chase)
        {
            Health = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<EnemyControl>().Chase)
        {
            Health = 0;
        }
    }

    IEnumerator Dying()
    {
        Debug.Log("Dying");
        // Play the dead animations
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetBool("dead", true);
        Traffic.Stop();
        PauseButton.SetActive(false);
        Background.Stop();

        if (!DieSound.isPlaying && !GameOverPanel.active)
            DieSound.Play();
        yield return new WaitForSeconds(3);
        DieSound.Stop();
        DisplayGameOverPanel();
    }

    private void DisplayGameOverPanel()
    {
        int highestScore = RestoreHighestScore();
        ScoreText.text = (Mathf.Max(gameObject.GetComponent<PlayerMoneyManage>().money, highestScore)).ToString();
        GameOverPanel.SetActive(true);
        CoinsPanel.SetActive(false);
        Time.timeScale = 0;
        SaveHighestScore(Mathf.Max(gameObject.GetComponent<PlayerMoneyManage>().money, highestScore));
    }

    /// <summary>
    /// Get the highest score saved
    /// </summary>
    /// <returns></returns>
    private int RestoreHighestScore()
    {
        string p = PlayerPrefs.GetString("PlayerHighestScore");
        if (p != null && p.Length > 0)
        {
            SaveScore s = JsonUtility.FromJson<SaveScore>(p);
            if (s != null)
                return s.HighestScore;
            else
                return -1;
        }
        else
            return -1;
    }

    /// <summary>
    /// Common example of serializing object to save data
    /// </summary>
    private void SaveHighestScore(int highest)
    {
        SaveScore highestScore = new SaveScore();
        highestScore.HighestScore = highest;

        string json = JsonUtility.ToJson(highestScore);
        PlayerPrefs.SetString("PlayerHighestScore", json);
    }
}
