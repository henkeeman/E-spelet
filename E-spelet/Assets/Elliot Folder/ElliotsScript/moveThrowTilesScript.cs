using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveThrowTilesScript : MonoBehaviour
{
    bool transparent;

    Vector3 tempPos;
    float temptimer;
    // Update is called once per frame
    void Update()
    {

        if (GetComponent<PlayerMovementPrototype>() == null)
            return;
        if(checkVelosityDown())
        {
            transparent = false;
        }
        else
        {
            transparent = true;
        }


        if(transparent)
        {
            gameObject.layer = 12;
        }
        else
        {
            gameObject.layer = 10;
        }

    }

    bool checkVelosityDown()
    {

        if (tempPos == null)
        {
            tempPos = transform.position;
            return true;
        }
        if (transform.position.y - tempPos.y < 0)
        {
            tempPos = transform.position;
            return true;
        }
        tempPos = transform.position;
        return false;
    }
}
