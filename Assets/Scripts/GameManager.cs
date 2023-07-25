using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //�̱��� ���ٿ� ������Ƽ
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
    private int health = 3;

    public int scoreThreshold = 1500;

    private int burgerTouchCount = 0; //���� ��ġ Ƚ���� ����ϴ� ����
    public int maxBurgerTouchCount = 3; //���� ���������� �Ѿ ���� ��ġ �ִ� Ƚ��

    public float LimitTime;

    public int lastSceneIndex = 3; //������ ���� ���� �ε���
    private int currentSceneIndex = 0; //���� ���� ���� �ε���

    public TextMeshProUGUI timeText;

    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //���� ���� ���� �ε����� ������ �ʱ�ȭ
    }

    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);

            CheckStage2Clear();
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

    public void RemoveHeart(int heart)
    {
        if (!isGameover)
        {
            health -= heart;
            UIManager.instance.UpdateHealthText(health);
        }
    }

    //�� ��ġ Ƚ���� ����ϰ�, ���Ÿ� ��ġ�� ������ Ƚ�� ����
    public void RecordBurgerTouch()
    {
        burgerTouchCount++;

        if (burgerTouchCount >= maxBurgerTouchCount)
        {
            //���� ���������� �Ѿ�� ���� �߰�
            NextStage();
        }
    }

    private void CheckStage2Clear()
    {
        if(score>=scoreThreshold || burgerTouchCount >= maxBurgerTouchCount)
        {
            NextStage();
        }
    }
    public void NextStage()
    {
        if (currentSceneIndex < lastSceneIndex) //���� ���� ������ ���� �ƴ϶�� NextStageUI ǥ��
        {
            UIManager.instance.SetActiveNextStageUI(true);
        }
        else
        {
            UIManager.instance.SetActiveNextStageUI(false);
            UIManager.instance.SetActiveMainUI(true);
        }

        UIManager.instance.SetActiveClearUI(true);

        isGameover = true;
    }

    void Update()
    {
        if (LimitTime <= 0 || health <= 0)
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
        UIManager.instance.SetActiveMainUI(true);
    }
}
