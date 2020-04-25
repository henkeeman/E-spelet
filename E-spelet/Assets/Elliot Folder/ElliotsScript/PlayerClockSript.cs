using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerClockSript : MonoBehaviour
{
    static float timer; // the timer
    public float StartTime;// so the player can change amount of seconds
    public TextMeshPro ScoreText;//visulice the time on the screen
    public bool Endgame;//när alla spelare e död
    public float AddedTimeOnCollitionWithClock;
    public float SubstractedTimeOnCollitionWithLava;
    public float LavaForce = 2;
    bool grow;//so the time can change it's size
    float count;//a timer for the growth change
    // Start is called before the first frame update
    void Start()
    {
        if(AddedTimeOnCollitionWithClock <= 0)
        {
            AddedTimeOnCollitionWithClock = 20;
        }
        Endgame = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Clock")
        {
            StartTime += AddedTimeOnCollitionWithClock;
            Destroy(other.gameObject);
        }
        if(other.tag == "Lava")
        {
            StartTime -= AddedTimeOnCollitionWithClock;
            GetComponent<Rigidbody>().AddForce(Vector3.up * LavaForce,ForceMode.Impulse);
        }
    }
   

    // Update is called once per frame
    void Update()
    {
      
        if (StartTime < 10)
        {
            printStressTime();
        }
        else
        {
            printMin();
        }

    }

    void printMin()
    {

        string sec;
        string timer;

        if(StartTime < 0.6f)
        {
            Endgame = true;
        }

        StartTime -= Time.deltaTime;

         if (StartTime <= 10)
      {
        //  ScoreText.fontSize = 5f;
      }
      

        sec = StartTime.ToString().Split(',')[0]; // so the text can show the time
        timer = (sec);

        if (Endgame)
        {
            timer = "GameOver";
        }

        ScoreText.text = timer;

    }

    void printStressTime()
    {
        string sec;
        string timer;

        if (StartTime < 0.6f)
        {
            Endgame = true;
        }

        bounse();

        StartTime -= Time.deltaTime;



     

        sec = StartTime.ToString().Split(',')[0]; // so the text can show the time
        timer = (sec);

        if (Endgame)
        {
            timer = "GameOver";
        }

        ScoreText.text = timer;
    }

    void bounse()
    {

            if (ScoreText.fontSize > 7)
            {
                grow = false;
            }


            if (ScoreText.fontSize < 3)
            {
                grow = true;
            }



            if (grow)
            {
                count += Time.deltaTime ;
                if (count > .1)
                {
                ScoreText.fontSize += 3 / Mathf.Sqrt(StartTime);
                    count = 0;
                }

            }
            else
            {
                count += Time.deltaTime;
                if (count > .1)
                {
                ScoreText.fontSize -= 3 / Mathf.Sqrt(StartTime);
                    count = 0;
                }
            }


    }

}
