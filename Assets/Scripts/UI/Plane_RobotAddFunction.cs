using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_RobotAddFunction : MonoBehaviour
{
    public Camera m_camera;
    public GameObject Robot;

    private RobotCreated robotInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnEnable()
    {
        Debug.Log("出现了！");
        m_camera.transform.position = new Vector3(0, 3, -4);
        m_camera.GetComponent<OutlineCommandBuffer>().OnChangeRenderObj(null);
        Rigidbody rigidbody = Robot.AddComponent<Rigidbody>();
        rigidbody.mass = GetMass(robotInfo);
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
        this.robotInfo = r;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
