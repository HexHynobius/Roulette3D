using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Title : MonoBehaviour
{

    [SerializeField]
    GameObject titleCamera1;
    [SerializeField]
    GameObject titleCamera2;

    [SerializeField]
    GameObject at;


    [SerializeField]
    Roulette rot;

    int tempTime = 1;



    private void Update()
    {


        if (tempTime % 20000 == 0)
        {
            at.transform.position = new Vector3(Random.Range(5, 21), Random.Range(0, 3), 0);
            titleCamera1.SetActive(true);
            titleCamera2.SetActive(false);
        }
        else if (tempTime % 10000 == 0)
        {
            at.transform.position = new Vector3(Random.Range(5, 21), Random.Range(0, 3), 0);
            titleCamera2.SetActive(true);
            titleCamera1.SetActive(false);
        }

        //找目標當中心點
        if (((tempTime / 10000) % 2 == 0))
        {
            titleCamera1.transform.position = new Vector3(at.transform.position.x + 10 * Mathf.Cos(Time.time * 0.3f), titleCamera1.transform.position.y, at.transform.position.z + 10 * Mathf.Sin(Time.time * 0.3f));
        }
        else if (((tempTime / 10000) % 2 == 1))
        {
            titleCamera2.transform.position = new Vector3(at.transform.position.x + 10 * Mathf.Cos(-Time.time * 0.3f), titleCamera2.transform.position.y, at.transform.position.z + 10 * Mathf.Sin(-Time.time * 0.3f));
        }
        //titleCamera.transform.rotation = 

        tempTime += 1;
        rot.rot();
    }



}
