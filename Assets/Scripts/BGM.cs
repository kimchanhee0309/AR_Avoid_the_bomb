using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public AudioClip bgmClip; //����� BGM ���� Ŭ��
    private AudioSource audioSource;
    private string targetSceneName = "SampleScene"; //BGM�� ���� Ư�� ���� �̸�
    private string targetSceneName1 = "stage2"; //BGM�� ���� Ư�� ���� �̸�

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject); //BGMManager ��ü�� �� ��ȯ �� �ı����� �ʵ��� ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        audioSource.clip = bgmClip; //BGM�� ����� Ŭ�� �Ҵ�
        audioSource.Play(); //BGM ���
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == targetSceneName)
        {
            audioSource.Stop(); //Ư�� ������ BGM ����
            Destroy(gameObject); //BGMManager ��ü �ı�
        }

        if (scene.name == targetSceneName1)
        {
            audioSource.Stop(); //Ư�� ������ BGM ����
            Destroy(gameObject); //BGMManager ��ü �ı�
        }
    }
}
