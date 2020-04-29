using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public int Id;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!Arcade.PlayerIsIngame(Id))
        {
            gameObject.SetActive(false);

            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Arcade.GetKey(Id, ArcadeButton.Left))
        {
            transform.Translate(new Vector3(-1, 0, 0)*Time.deltaTime);
            Arcade.AddScore(Id, 3);
            Arcade.GetScore(Id);
            
        }
        if (Arcade.GetKey(Id, ArcadeButton.Right))
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
            Arcade.AddScore(Id, 3);
            Arcade.GetScore(Id);

        }
        if (Arcade.GetKey(Id, ArcadeButton.Up))
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
            Arcade.AddScore(Id, 3);
            Arcade.GetScore(Id);

        }
        if (Arcade.GetKey(Id, ArcadeButton.Down))
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
            Arcade.AddScore(Id, 3);
            Arcade.GetScore(Id);

        }

       
        //Arcade.Save();
    }
    
}
