using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Quaternion quatCamera = transform.rotation;
        Vector3 posCamera = transform.position;
        transform.SetPositionAndRotation(posCamera, quatCamera);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, Time.deltaTime * 20f);
        transform.LookAt(new Vector3(0, 0, 0));
    }
}
