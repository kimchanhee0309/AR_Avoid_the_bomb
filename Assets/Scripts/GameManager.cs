using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private int health = 3;

    public int scoreThreshold = 1500;

    private int burgerTouchCount = 0; //버거 터치 횟수를 기록하는 변수
    public int maxBurgerTouchCount = 3; //다음 스테이지로 넘어갈 버거 터치 최대 횟수

    public float LimitTime;

    public int lastSceneIndex = 3; //마지막 씬의 빌드 인덱스
    private int currentSceneIndex = 0; //현재 씬의 빌드 인덱스

    public TextMeshProUGUI timeText;

    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //현재 씬의 빌드 인덱스를 가져와 초기화
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

    //총 터치 횟수를 기록하고, 버거를 터치할 때마다 횟수 증가
    public void RecordBurgerTouch()
    {
        burgerTouchCount++;

        if (burgerTouchCount >= maxBurgerTouchCount)
        {
            //다음 스테이지로 넘어가는 로직 추가
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
        if (currentSceneIndex < lastSceneIndex) //현재 씬이 마지막 씬이 아니라면 NextStageUI 표시
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
