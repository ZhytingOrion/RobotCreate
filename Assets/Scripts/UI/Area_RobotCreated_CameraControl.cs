using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area_RobotCreated_CameraControl : MonoBehaviour
{
    public Slider XRot;
    public Slider YRot;
    public Slider UpLoc;
    public Camera cam;
    public GameObject robot;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = cam.transform.rotation.eulerAngles;
        XRot.value = rot.x % 360.0f;
        YRot.value = rot.y % 360.0f;
        UpLoc.value = cam.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraRot()
    {
        cam.transform.rotation = Quaternion.Euler(XRot.value, YRot.value, 0.0f);
        float distance = GetDistance(robot.transform.position.x, robot.transform.position.z, cam.transform.position.x, cam.transform.position.z);
        float Y_Radian = YRot.value * Mathf.PI / 180.0f;
        cam.transform.position = new Vector3(-Mathf.Sin(Y_Radian) * distance, cam.transform.position.y, -Mathf.Cos(Y_Radian) * distance);
    }

    private float GetDistance(float x, float y, float x1, float y1)
    {
        return Mathf.Sqrt((x-x1) * (x-x1) + (y-y1) * (y-y1));
    }

    public void SetCameraDistance(float step)
    {
        cam.transform.position += cam.transform.forward * step;
    }

    public void SetCameraUpLoc()
    {
        Vector3 pos = cam.transform.position;
        cam.transform.position = new Vector3(pos.x, UpLoc.value, pos.z);
    }
}
