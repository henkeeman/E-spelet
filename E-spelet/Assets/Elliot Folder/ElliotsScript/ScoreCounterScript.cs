using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounterScript : MonoBehaviour
{
    static float timer; // the timer

    float startMin; // amount of minutes you start with
    float temp;
    int n; // n = number

    static public float Sec;// so the player can change amount of seconds
    public TextMeshProUGUI ScoreText;//visulice the time on the screen
    // Start is called before the first frame update
    void Start()
    {
        Sec = startMin;
    }

    // Update is called once per frame
    void Update()
    {
        printMin();
    }

    void printMin()
    {

        string sec;
        string timer;
        counter();

        sec = Sec.ToString().Split(',')[0]; // so the text can show the time
            timer = (sec + " :Score");
         ScoreText.text = timer;

    }

    void counter()
    {

        if (temp > .5f)
        {
            n++;
            Sec = 50 * n;
            temp = 0;
        }
        else
            temp += Time.deltaTime;
    }

}
