using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PositionFollow : MonoBehaviour
{

    public Transform targetTransform;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = targetTransform.position + offset;
    }
}
