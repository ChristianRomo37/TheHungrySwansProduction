using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerControl : MonoBehaviour
{
    [SerializeField] int sensHor;
    [SerializeField] int sensVert;

    [SerializeField] int lockVermin;
    [SerializeField] int lockVermax;

    [SerializeField] bool invertY;

    float xrotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensVert;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensHor;

        if (invertY)
        {
            xrotation += mouseY;
        }
        else
        {
            xrotation -= mouseY;
        }

        xrotation = Mathf.Clamp(xrotation, lockVermin, lockVermax);

        transform.localRotation = Quaternion.Euler(xrotation, 0, 0);

        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
