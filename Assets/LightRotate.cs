using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightRotate : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;
    void Start()
    {
        Quaternion quatCamera = transform.rotation;
        Vector3 posCamera = transform.position;
        transform.SetPositionAndRotation(posCamera, quatCamera);
    }

    // Update is called once per frame
    void Update()
    {
        //speed = GameObject.Find("length").GetComponent<Slider>().value/2;
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.forward, Time.deltaTime * 20f);
    }
}
