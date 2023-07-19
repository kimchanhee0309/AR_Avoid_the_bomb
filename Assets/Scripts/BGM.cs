using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public AudioClip bgmClip; //재생할 BGM 비디오 클립
    private AudioSource audioSource;
    private string targetSceneName = "SampleScene"; //BGM을 끊을 특정 씬의 이름

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject); //BGMManager 객체를 씬 전환 시 파괴하지 않도록 설정
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        audioSource.clip = bgmClip; //BGM에 오디오 클립 할당
        audioSource.Play(); //BGM 재생
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == targetSceneName)
        {
            audioSource.Stop(); //특정 씬에서 BGM 중지
            Destroy(gameObject); //BGMManager 객체 파괴
        }
    }
}
