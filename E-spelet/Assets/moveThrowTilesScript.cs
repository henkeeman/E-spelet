using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveThrowTilesScript : MonoBehaviour
{
    bool transparent;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerMovementPrototype>() == null)
            return;

        transparent = GetComponent<PlayerMovementPrototype>().Grounded;
        if(!transparent)
        {
            gameObject.layer = 12;
        }
        else
        {
            gameObject.layer = 10;
        }
    }
}
