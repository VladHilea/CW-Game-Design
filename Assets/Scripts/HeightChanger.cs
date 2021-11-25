using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChanger : MonoBehaviour
{
    [SerializeField] [Range(0f, 3f)] float lerpTime;
    float height = 0f;
    float yValue;
    void Update()
    {
        //doesn't quite work
        height = Mathf.Lerp(0.5f, 3f, lerpTime*Time.deltaTime);
        Vector3 newPosition = new Vector3();
        newPosition.x = transform.position.x;
        newPosition.y = height;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
