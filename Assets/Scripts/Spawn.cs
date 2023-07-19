using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Spawn : MonoBehaviour
{
    public Transform[] pos;
    public GameObject[] prefab;
    AudioSource audio;
    ARSession arSession;
    ARCameraManager arCameraManager;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        arSession = FindObjectOfType<ARSession>();
        arCameraManager = FindObjectOfType<ARCameraManager>();

        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        while (true)
        {

            float waitTime = Random.Range(2.0f, 4.0f);
            yield return new WaitForSeconds(waitTime);

            for(int i=0; i<4; i++)
            {
                if (GameManager.instance.isGameover)
                {
                    yield break;
                }

                int iPrefab = Random.Range(0, prefab.Length);
                int iPos = Random.Range(0, pos.Length);

                GameObject obj = Instantiate(prefab[iPrefab], pos[iPos].position, Quaternion.identity);

                Destroy(obj, 5f);

                Rigidbody rb = obj.GetComponent<Rigidbody>();

               Vector3 cameraPosition = arCameraManager.transform.position;
               Vector3 cameraForward = arCameraManager.transform.forward;
               Vector3 spawnPosition = cameraPosition + cameraForward * Random.Range(4.0f, 10.0f);

               //Vector3 direction = spawnPosition - obj.transform.position;
               
                Vector3 direction = -Camera.main.transform.forward;
                rb.AddForce(direction.normalized * Random.Range(4.0f, 6.0f), ForceMode.VelocityChange);

            }
            audio.Play();
        }
    }

}
