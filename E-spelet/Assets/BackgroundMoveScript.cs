using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{
    private float lenght, startpos;
    public GameObject cam;
    public GameObject PrefabTile;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Gravity()
    {
        transform.position -= new Vector3(0, (PrefabTile.GetComponent<TIleScript>().Mass * PrefabTile.GetComponent<TIleScript>().PullForse) * Time.deltaTime, 0);

        //PullForse = PullForse * 1.1f; Kändes wierd
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gravity();

        /*
        float temp = (cam.transform.position.y * (1 - parallaxEffect));
        float dist = (cam.transform.position.y * parallaxEffect);
        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);*/

        if (transform.position.y < startpos - (lenght / 2))
            transform.position = new Vector3(transform.position.x, startpos, transform.position.z);



       // print("pos y: " + transform.position.y);
        print("pos: " + (startpos - lenght));
    }
}
