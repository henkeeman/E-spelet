using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIleScript : MonoBehaviour
{
    public GameObject ClockPrefab;

    public float SpawnValueOfClocks;
    public float DividerMaxLimitOfClock;

    static float OddsOfSpawnigClock;
    static float DividerMaxLimit;
    static float SpawnValue;
    public float PullForse;
    public float Mass;
    // Start is called before the first frame update
    void Start()
    {
        SpawnValue = (SpawnValueOfClocks + 1);
        DividerMaxLimit = DividerMaxLimitOfClock ;
       // OddsOfSpawnigClock = 1;
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

       // PullForse = PullForse * 1.1f; //Kändes wierd
    }


    void oddsOfSpawningClock()
    {
        if(SpawnValue < OddsOfSpawnigClock)
        {
            OddsOfSpawnigClock = 1;
            Vector3 postemp;
            postemp = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y + .5f), transform.position.z);
            Instantiate(ClockPrefab,postemp,Quaternion.identity);
        }
        else
        {
            if (SpawnTileScript.WhatRowLookingFor < DividerMaxLimit)
                OddsOfSpawnigClock += (1 / Mathf.Sqrt(SpawnTileScript.WhatRowLookingFor) * Time.deltaTime);
            else
                OddsOfSpawnigClock += (1 / Time.deltaTime * Mathf.Sqrt(DividerMaxLimit));
        }

        //print("odds of spawning clocks: " + OddsOfSpawnigClock);

    }
}
