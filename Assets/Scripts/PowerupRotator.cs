using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupRotator : MonoBehaviour
{
    MeshRenderer CubeMeshRenderer;
    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float t = 0f;
    int len;

    void Start()
    {
        CubeMeshRenderer = GetComponent<MeshRenderer>();  
        len =  myColors.Length;
    }

    void Update()
    {
        CubeMeshRenderer.material.color = Color.Lerp(CubeMeshRenderer.material.color, myColors[colorIndex], Time.deltaTime);
        t = Mathf.Lerp(t, 1f, Time.deltaTime);
        if(t>.9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex; 
        }
        
        transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
        
    }
}
