using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParicalEffectScript : MonoBehaviour
{

    public float Time;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Time);   
    }

    
}
