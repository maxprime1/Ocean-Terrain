using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraChooser : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera overheadCamera;
    private int c = 0;
    public Text camMode;
    // Call this function to disable FPS camera,
    // and enable overhead camera.
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            c++;
        if (c % 2 == 0)
            ShowFirstPersonView();
        else
            ShowOverheadView();
    }
    public void ShowOverheadView()
    {
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
        camMode.text = "Overhead Mode";
    }

    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void ShowFirstPersonView()
    {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
        camMode.text = "First Person Mode";
    }
}
