using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_RobotAddFunction : MonoBehaviour
{
    public Camera m_camera;
    public GameObject Robot;
    public float RobotRotateSpeed = 10.0f;

    private RobotCreated m_robotInfo;
    private bool m_isRobotRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnEnable()
    {
        m_camera.transform.position = new Vector3(0, 3, -4);
        m_camera.GetComponent<OutlineCommandBuffer>().OnChangeRenderObj(null);
        Rigidbody rigidbody = Robot.AddComponent<Rigidbody>();
        rigidbody.mass = GetMass(m_robotInfo);
        Robot.transform.localPosition += Vector3.up * 3.0f;
    }

    public float GetMass(RobotCreated r)
    {
        float mass = 0.0f;
        if (r == null) return 3.0f;
        int number = r.RobotComponents.Count;
        for(int i = 0; i<number; ++i)
        {
            mass += GetComponentMass(r.RobotComponents[i].prefabName);
        }
        return mass;
    }

    public float GetComponentMass(string prefabName)
    {
        //todo
        return prefabName.Length;
    }

    public void SetRobotInfo(RobotCreated r)
    {
        this.m_robotInfo = r;
    }

    public void SetRobotRotate(bool isRotate)
    {
        this.m_isRobotRotate = isRotate;
    }

    public void ResetRobotRotate()
    {
        Robot.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isRobotRotate)
        {
            Robot.transform.Rotate(Vector3.up, RobotRotateSpeed * Time.deltaTime);
        }
    }
}
