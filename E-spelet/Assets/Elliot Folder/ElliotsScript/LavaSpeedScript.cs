﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpeedScript : MonoBehaviour
{
    public float Mass;
    public float PullForse;
    public float MaxSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
            Destroy(gameObject);
        Gravity();
    }

    void Gravity()
    {
        transform.position -= new Vector3(0, ((Mass * PullForse) * Time.deltaTime), 0);
        //print(transform.position.y);
        if (transform.position.y > 100)
            PullForse = MaxSpeed;
        else 
        PullForse = Mathf.Lerp(1, MaxSpeed, transform.position.y / 100);// Kändes wierd
    }
}
