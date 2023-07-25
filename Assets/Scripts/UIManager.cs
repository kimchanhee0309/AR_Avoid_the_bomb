using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    private static UIManager m_instance; //싱글턴이 할당될 변수

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    public GameObject gameoverUI;
    public GameObject RestartUI;
    public GameObject MainUI;
    public GameObject NextStageUI;
    public GameObject clearUI;
    public int sceneNumber;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    public void UpdateHealthText(int heart)
    {
        healthText.text = "x " + heart;
    }

    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void SetActiveRestartUI(bool active)
    {
        RestartUI.SetActive(active);
    }

    public void SetActiveMainUI(bool active)
    {
        MainUI.SetActive(active);
    }

    public void SetActiveNextStageUI(bool active)
    {
        NextStageUI.SetActive(active);
    }

    public void SetActiveClearUI(bool active)
    {
        clearUI.SetActive(active);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
