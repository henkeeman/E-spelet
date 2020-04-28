using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Camera))]
public class CameraSmootherScript : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float SmoothTime = .5f;
    public float MaxZoom = 50;
    public float MinZoom = 30;
    public float ZoomLimiter = 1;

    int old;
    bool first;
    Camera Cam;
    Vector3 velocity;

    private void Start()
    {
        /* for (int i = 0; i < targets.Count; i++)
         {
             if (targets[i].GetComponent<Transform>().== false)
                 targets.Remove(targets[i]);
         }*/
        first = false;
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        add();
        remove();

        if (targets.Count == 0)
            return;
        movment();
        Zoom();
    }

    void add()
    {
        
        if(targets.Count == 0 && !first)
        {
            int temp = GetComponent<SpawnPlayerOnStartScript>().PlayerList.Count;
            for (int i = 0; i < temp; i++)
            {
                targets.Add(GetComponent<SpawnPlayerOnStartScript>().PlayerList[i].transform);
            }
        }
    }

    void remove()
    {
        if (targets.Count == 0)
            return;
        else
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                    targets.Remove(targets[i]);

            }
        }
    }

    void Zoom()
    {
         float newZoomx = Mathf.Lerp(MinZoom, MaxZoom, getGreatestDisctance().y/ ZoomLimiter);

        Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, newZoomx, Time.deltaTime);
        first = true;
       // Debug.Log(getGreatestDisctance());
    }

    Vector3 topTarget()
    {
        int top = 0;

        for (int i = 0; i < targets.Count; i++)
        {

           if(i == 0)
                top = 0;
            else if(targets[i].transform.position.y > targets[i - 1].transform.position.y)
            {
                top = i;
            }
        }

        return targets[top].transform.position;
    }

    Vector3 getGreatestDisctance()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size;
    }

   /* void topPlayerMove()
    {
        Vector3 smoothedPosition = Vector3.Lerp(topTarget(), (topTarget() + offset), .0125f);
        print("top player pos" + smoothedPosition);
      //  Cam.transform.position = smoothedPosition;

    }*/

    void movment()
    {

        Vector3 Centerpoint = centerPoint();

        Vector3 newpos = Centerpoint + offset;
        //print("newpos" + newpos);
        transform.position = Vector3.SmoothDamp(transform.position, newpos, ref velocity, SmoothTime/ZoomLimiter);
    }


    Vector3 centerPoint()
    {
        if (targets.Count == 0)
            return Vector3.zero;
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
