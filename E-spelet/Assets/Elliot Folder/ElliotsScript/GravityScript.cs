using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public float Mass;
    public float PullForse;


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
        print(transform.position.y);
        PullForse = Mathf.Lerp(1,4,transform.position.y/100);// Kändes wierd
    }
}
