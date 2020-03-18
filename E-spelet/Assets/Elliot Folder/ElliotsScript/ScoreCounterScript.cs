using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounterScript : MonoBehaviour
{
    static float timer; // the timer
    public float MaxValue;
    public float Konstant;
    float startMin; // amount of minutes you start with
    public float Sec;// so the player can change amount of seconds
    public TextMeshProUGUI ScoreText;//visulice the time on the screen
    // Start is called before the first frame update
    void Start()
    {
        startMin = Sec;
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
        Sec += Time.deltaTime * addedCounter(); 


        sec = Sec.ToString().Split(',')[0]; // so the text can show the time
            timer = (sec + " :Score");
        if((Sec/ 2)  % 5 < .5)
         ScoreText.text = timer;

    }

    float addedCounter()
    {
        /*
        float x = Mathf.Pow(2,-Konstant * Sec);
        CountAdder = MaxValue / (1 + (MaxValue * x));
        print(CountAdder);
        return CountAdder;
         */

        float x = 50 * (Konstant * Mathf.Sqrt(Sec));
        return x;

    }
}
