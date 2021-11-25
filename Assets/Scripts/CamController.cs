using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject trackedObject1;
    public GameObject trackedObject2;
    public GameObject target1;
    public GameObject target2;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - (trackedObject1.transform.position + trackedObject2.transform.position + target1.transform.position + target2.transform.position)/4;
    }

    // Update is called once per frame
    void LateUpdate() //runs like Update but updates the position after all other updates like physics and stuff are done
    {
        /*float xValue = player.transform.position.x / 25;
        float zValue = player.transform.position.z / 25;
        Vector3 extraOffset = new Vector3(xValue, transform.position.y, zValue);*/
        transform.position = (trackedObject1.transform.position + trackedObject2.transform.position + target1.transform.position + target2.transform.position)/4 + offset;
    }
}