using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounterScript : MonoBehaviour
{
    static float timer; // the timer
    float startMin; // amount of minutes you start with
    public float Sec;// so the player can change amount of seconds
    public TextMeshProUGUI ScoreText;//visulice the time on the screen
    public static bool Endgame;//när alla spelare e död
    // Start is called before the first frame update
    void Start()
    {
        startMin = Sec;
        Endgame = false;
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
        Sec += Time.deltaTime;


        sec = Sec.ToString().Split(',')[0]; // so the text can show the time
            timer = (sec + " :Score");
        if(Endgame)
            timer = "GameOver";
        ScoreText.text = timer;

    }
}
