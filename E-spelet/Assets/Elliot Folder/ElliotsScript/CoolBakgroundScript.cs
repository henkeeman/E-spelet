using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolBakgroundScript : MonoBehaviour
{
    public Color[] ColorBank; // gose from color 1 to color n
    public float KonstantOfChange = 1;

    Camera mainCamera;
    SpriteRenderer child1;
    float x;
    int n; //color number
    int posOfChange;

    // Start is called before the first frame update
    void Start()
    {
        child1 = transform.Find("Layer1").GetComponent<SpriteRenderer>();

        mainCamera = Camera.main;

        posOfChange = 0;
        n = 0;

    }

    // Update is called once per frame
    void Update()
    {
        position();
        ChangeColors();
    }

    void position()
    {
        x = mainCamera.transform.position.y * KonstantOfChange;
    }

    void ChangeColors()
    {
        if(x > posOfChange)
        {
            posOfChange += 5;

            if (n + 2 < ColorBank.Length)
                n++;
            else
                n = 0;
        }
        child1.color = Color.Lerp(ColorBank[n], ColorBank[n + 1], Mathf.Abs(Mathf.Sin(x)));
        child1.color = new Color(child1.color.r, child1.color.g, child1.color.b);

       // print(n);


        //Mathf.Lerp(0, 1, Mathf.Sin(x));
    }

   
}
