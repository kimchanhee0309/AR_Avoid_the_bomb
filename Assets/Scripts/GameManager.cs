using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //싱글턴 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    private static GameManager m_instance;

    private int score = 0;
    public float LimitTime;
    public TextMeshProUGUI timeText;

    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);
        }
    }

    public void RemoveScore(int newScore)
    {
        if (!isGameover)
        {
            score -= newScore;
            UIManager.instance.UpdateScoreText(score);
        }
    }

    void Update()
    {
        if (LimitTime <= 0)
        {
            EndGame();
        }

        if (!isGameover)
        {
            LimitTime -= Time.deltaTime;
            timeText.text = "Time : " + Mathf.Round(LimitTime);
        }
    }

    public void EndGame()
    {
        isGameover = true;
        UIManager.instance.SetActiveGameoverUI(true);
        UIManager.instance.SetActiveRestartUI(true);
    }
}
