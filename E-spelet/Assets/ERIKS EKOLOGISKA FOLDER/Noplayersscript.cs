using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Security.Cryptography;
using UnityEngine;


public class Noplayersscript : MonoBehaviour
{

    // Update is called once per frame

    float time = 0;
    public float StartTime = 2;
    static public int Score = 0;

    //void Awake()
    //{
    //    DontDestroyOnLoad(transform.gameObject);
    //}

    void LateUpdate()
    {

        if (time > StartTime)
        {
            if (GetComponent<CameraSmootherScript>().targets == null)
                return;

            else if (GetComponent<CameraSmootherScript>().targets.Count == 0)

            {

                Application.LoadLevel("spelarmeny");
            }
        }
        else
        {
            
            time += Time.deltaTime;

        }
    }
}
