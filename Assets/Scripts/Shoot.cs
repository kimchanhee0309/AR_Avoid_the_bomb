using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Camera camera;
    public GameObject prefab1;
    public GameObject prefab2;
    public AudioClip eatClip;
    public AudioClip BombClip;

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
                audio.PlayOneShot(eatClip);
            }

            if (hit.transform.CompareTag("Bomb"))
            {
                Destroy(hit.transform.gameObject);
                Instantiate(prefab2, hit.point, Quaternion.LookRotation(hit.normal));
                GameManager.instance.RemoveScore(50);
                audio.PlayOneShot(BombClip);
            }
        }
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
