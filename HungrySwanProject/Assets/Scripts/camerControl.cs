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

    /*[Header("----- Shake -----")]
    [SerializeField, Range(0.01f,1f)] float shakeX = 0.01f;
    [SerializeField, Range(0.01f, 1f)] float shakeY = 0.01f;
    [SerializeField, Range(0f, 3f)] float shakeSpeed;*/
    Vector3 posOrig;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        posOrig = transform.position;
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

        /*if (gameManager.instance.playerScript.isSprinting == true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Random.Range(-shakeX, shakeX), Random.Range(-shakeY, shakeY), transform.position.z), Time.deltaTime * shakeSpeed);
        }
        else
            transform.position = posOrig;
            //transform.position = Vector3.Lerp(transform.position, posOrig, Time.deltaTime * shakeSpeed);*/
    }

    public void ApplyRecoil(float recoilAmount)
    {
        float recoilRotation = -recoilAmount * Time.deltaTime * sensVert;

        if (invertY)
        {
            xrotation -= recoilRotation;
        }
        else
        {
            xrotation += recoilRotation;
        }

        xrotation = Mathf.Clamp(xrotation, lockVermin, lockVermax);

        transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
    }
}
