using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticalSpread : MonoBehaviour
{

    private RectTransform ret;
    public CharacterController characterController;
    public float resting;
    public float maxSize;
    public float speed;
    public float curSize;

    // Start is called before the first frame update
    void Start()
    {
        ret = GetComponent<RectTransform>();
        characterController = gameManager.instance.playerScript.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving || characterController.velocity.sqrMagnitude != 0 || gameManager.instance.playerScript.isShooting)
        {
            curSize = Mathf.Lerp(curSize,maxSize, Time.deltaTime * speed);
        }
        else
        {
            curSize = Mathf.Lerp(curSize, resting, Time.deltaTime * speed);
        }
        ret.sizeDelta = new Vector2(curSize, curSize);
    }

    bool isMoving
    {
        get
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
