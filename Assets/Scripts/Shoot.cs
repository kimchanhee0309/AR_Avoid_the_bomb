using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{

    public Camera camera;
    public GameObject prefab1;
    public GameObject prefab2;

    public AudioClip eatClip;
    public AudioClip BombClip;

    public GameObject imgDamage;

    public float damageDuration = 0.5f; //�ǰ� �� �̹����� Ȱ��ȭ�Ǵ� �ð�

    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Soda"))
            {
                Destroy(hit.transform.gameObject);
                Instantiate(prefab1, hit.point, Quaternion.LookRotation(hit.normal));
                GameManager.instance.AddScore(50);
                audio.PlayOneShot(eatClip);
            }

            if (hit.transform.CompareTag("French"))
            {
                Destroy(hit.transform.gameObject);
                Instantiate(prefab1, hit.point, Quaternion.LookRotation(hit.normal));
                GameManager.instance.AddScore(100);
                audio.PlayOneShot(eatClip);
            }

            if (hit.transform.CompareTag("Burger"))
            {
                Destroy(hit.transform.gameObject);
                Instantiate(prefab1, hit.point, Quaternion.LookRotation(hit.normal));
                GameManager.instance.AddScore(200);
                GameManager.instance.RecordBurgerTouch();
                audio.PlayOneShot(eatClip);
            }

            if (hit.transform.CompareTag("Bomb"))
            {
                Destroy(hit.transform.gameObject);
                Instantiate(prefab2, hit.point, Quaternion.LookRotation(hit.normal));
                GameManager.instance.RemoveScore(50);
                GameManager.instance.RemoveHeart(1);
                audio.PlayOneShot(BombClip);

                //�ǰ� �� �̹����� Ȱ��ȭ�� �� ���� �ð� �ڿ� �ٽ� ��Ȱ��ȭ
                StartCoroutine(ShowDamageImage());
            }
        }
    }

    private IEnumerator ShowDamageImage()
    {
        imgDamage.SetActive(true);
        yield return new WaitForSeconds(damageDuration);
        imgDamage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Fire();
            }
        }
    }
}
