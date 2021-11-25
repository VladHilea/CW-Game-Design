using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    
    public float smoothChange = .6f;

    private Vector3 velocity;

    public float minimumZoomY = 7f;
    public float maximumZoomY = 18.5f;

    public float minimumZoomZ = -3.5f;
    public float maximumZoomZ = -16.5f;


    private void LateUpdate() {

        if(targets.Count == 0)
            return;
        
        Move();
        Zoom();
       

    }

    void Move(){
        Vector3 centerPoint = GetCenterPoint();
         float a = GetGreaterDistance();

        Vector3 newPosition = centerPoint + offset;
        

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothChange);

    }

    void Zoom(){
        /*
        Debug.Log(GetGreaterDistance());
        float a = GetGreaterDistance();

        float newZoomX = Mathf.Lerp(maximumZoomZ, minimumZoomZ, a );
        float newZoomY = Mathf.Lerp(maximumZoomY, minimumZoomY, a );
        offset.x = newZoomX;
        offset.y = newZoomY;

        transform.position= offset;

        Vector3 centerPoint = GetCenterPoint();


        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothChange);
        */

    }

    float GetGreaterDistance(){
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0 ;i<targets.Count;i++){
            bounds.Encapsulate(targets[i].position);
        }

        if(bounds.size.x > bounds.size.z){
            return bounds.size.x;
        } else {
            return bounds.size.z;
        }
    }

    Vector3 GetCenterPoint(){

        if( targets.Count == 1){
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for ( int i = 0;i< targets.Count; i++){
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;

    }
}
