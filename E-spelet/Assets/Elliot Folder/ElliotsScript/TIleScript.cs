using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIleScript : MonoBehaviour
{
    public GameObject ClockPrefab;
    public static float OddsOfSpawnigClock;
    public float DividerMaxLimit;
    public float SpawnValue;
    public float PullForse;
    public float Mass;
    // Start is called before the first frame update
    void Start()
    {
        oddsOfSpawningClock();        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -7)
            Destroy(gameObject);
        Gravity();
    }

    void Gravity()
    {
        transform.position -= new Vector3(0, (Mass * PullForse) * Time.deltaTime, 0);

        //PullForse = PullForse * 1.1f; Kändes wierd
    }

    bool oddsOfSpawningClock()
    {
        if(SpawnValue < OddsOfSpawnigClock)
        {
            OddsOfSpawnigClock = 0;

            Instantiate(ClockPrefab,transform.position,Quaternion.identity);
        }
        else
        {
            if(SpawnTileScript.WhatRowLookingFor < DividerMaxLimit)
            OddsOfSpawnigClock += (Time.deltaTime / Mathf.Sqrt(SpawnTileScript.WhatRowLookingFor));
        }
        return false;
    }
}
