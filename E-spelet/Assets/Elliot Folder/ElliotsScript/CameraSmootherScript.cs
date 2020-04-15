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

    Camera Cam;
    Vector3 velocity;

    private void Start()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].GetComponent<Transform>() == null)
                targets.Remove(targets[i]);
        }
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        movment();
        Zoom();
    }

    void Zoom()
    {
         float newZoomx = Mathf.Lerp(MinZoom, MaxZoom, getGreatestDisctance()/ ZoomLimiter);

        Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, newZoomx, Time.deltaTime);

       // Debug.Log(getGreatestDisctance());
    }

    float getGreatestDisctance()
    {
        if(targets.Count == 1)
        {
            return 20;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.y;
    }


    void movment()
    {

        Vector3 Centerpoint = centerPoint();

        Vector3 newpos = Centerpoint + offset;
        print("newpos" + newpos);
        transform.position = Vector3.SmoothDamp(transform.position, newpos, ref velocity, SmoothTime/ZoomLimiter);
    }

    Vector3 centerPoint()
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

        return bounds.center;
    }
}
