using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerId;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

        if (!Arcade.PlayerIsIngame(PlayerId))
        {
            transform.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*  if (Arcade.GetKeyDown(PlayerId, ArcadeButton.Left))
          {
              velocity = new Vector3(-1, velocity.y, velocity.z);
          }
          if (Arcade.GetKeyDown(PlayerId, ArcadeButton.Right))
          {
              velocity = new Vector3(1, velocity.y, velocity.z);
          }
          if (Arcade.GetKeyDown(PlayerId, ArcadeButton.Up))
          {
              velocity = new Vector3(velocity.x, 1, velocity.z);
          }
          if (Arcade.GetKeyDown(PlayerId, ArcadeButton.Down))
          {
              velocity = new Vector3(velocity.x, -1, velocity.z);
          }*/


        /* if (Arcade.GetKeyUp(PlayerId, ArcadeButton.Left))
         {
             velocity = new Vector3(0, velocity.y, velocity.z);
         }
         if (Arcade.GetKeyUp(PlayerId, ArcadeButton.Right))
         {
             velocity = new Vector3(0, velocity.y, velocity.z);
         }
         if (Arcade.GetKeyUp(PlayerId, ArcadeButton.Up))
         {
             velocity = new Vector3(velocity.x, 0, velocity.z);
         }
         if (Arcade.GetKeyUp(PlayerId, ArcadeButton.Down))
         {
             velocity = new Vector3(velocity.x, 0, velocity.z);
         }*/


        //transform.Translate(velocity * Time.deltaTime);


        if (Arcade.GetKey(PlayerId, ArcadeButton.Left))
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        }
        if (Arcade.GetKey(PlayerId, ArcadeButton.Right))
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }
        if (Arcade.GetKey(PlayerId, ArcadeButton.Up))
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
        }
        if (Arcade.GetKey(PlayerId, ArcadeButton.Down))
        {
            transform.Translate(new Vector3(-0, -1, 0) * Time.deltaTime);
        }

        GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        if (Arcade.GetKey(PlayerId, ArcadeButton.Red))
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            Arcade.AddScore(PlayerId, 50);
            Arcade.SetScore(PlayerId, 13003);
            Arcade.SubtractScore(PlayerId, 100);
            Arcade.GetScore(PlayerId);
        }
        if (Arcade.GetKey(PlayerId, ArcadeButton.Green))
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            Arcade.AddScore(PlayerId, 137);
        }
        if (Arcade.GetKey(PlayerId, ArcadeButton.Blue))
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        if (Arcade.GetKey(PlayerId, ArcadeButton.Yellow))
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Arcade.Save();
        }

    }
}
