using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PositionFollow))]
public class ViewBobbing : MonoBehaviour
{
    public float eIntensity;
    public float eIntensityX;
    public float effectSpeed;

    private PositionFollow followerInstance;
    private Vector3 originalOffset;
    private float sinTime;


    // Start is called before the first frame update
    void Start()
    {
        followerInstance = GetComponent<PositionFollow>();
        originalOffset = followerInstance.offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
        if (inputVector.magnitude > 0f)
        {
            sinTime += Time.deltaTime * effectSpeed;
        }
        else
        {
            sinTime = 0f;
        }

        float sinAmountY = -Mathf.Abs(eIntensity * Mathf.Sin(sinTime));
        Vector3 sinAmountX = followerInstance.transform.right * eIntensity * Mathf.Cos(sinTime) * eIntensityX;

        followerInstance.offset = new Vector3
        {
            x = originalOffset.x,
            y = originalOffset.y + sinAmountY,
            z = originalOffset.z
        };

        followerInstance.offset += sinAmountX;
    }
}
