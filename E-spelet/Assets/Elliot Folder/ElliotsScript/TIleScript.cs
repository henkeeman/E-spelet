using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIleScript : MonoBehaviour
{
    public float PullForse;
    public float Mass;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
