using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip_Fall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Check()
    {
        GameObject.Find("Robot").GetComponent<RobotController>().ResetRobot();
        Destroy(this.gameObject);
    }

    public void Cancel()
    {
        Destroy(this.gameObject);
        RobotsCreated robots = RobotCreateTool.Instance.GetRobotsCreated();
        Debug.Log("Robots数量：" + robots.RobotCreated.Count);
        for (int i = 0; i < robots.RobotCreated.Count; i++)
        {            
            GameObject root = Instantiate(new GameObject());
            root.name = "RobotCreate_" + i;
            root.AddComponent<Rigidbody>();
            RobotCreated robot = robots.RobotCreated[i];
            for(int j = 0; j<robot.RobotComponents.Count; j++)
            {
                RobotCreatedComponent comp = robot.RobotComponents[j];
                GameObject obj = Instantiate(Resources.Load<GameObject>("RobotComponents/" + comp.prefabName), root.transform);
                obj.transform.localPosition = comp.pos;
                obj.transform.localRotation = Quaternion.Euler(comp.rot);
                obj.transform.localScale = comp.scale;
                //root = CopyCollider(obj, root);
            }
            root.transform.position = new Vector3(0, 3, 0);
            root.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public GameObject CopyCollider(GameObject from, GameObject to)
    {
        foreach(SphereCollider sc in from.GetComponents<SphereCollider>())
        {
            if (sc != null)
            {
                SphereCollider new_sc = to.AddComponent<SphereCollider>();
                new_sc.radius = sc.radius;
                new_sc.center = sc.center + from.transform.localPosition;
                Destroy(sc);
            }
        }
        foreach (BoxCollider bc in from.GetComponents<BoxCollider>())
        {
            if (bc != null)
            {
                BoxCollider new_bc = to.AddComponent<BoxCollider>();
                new_bc.size = bc.size;
                new_bc.center = bc.center;
                Destroy(bc);
            }
        }
        foreach (CapsuleCollider cc in from.GetComponents<CapsuleCollider>())
        {
            if (cc != null)
            {
                CapsuleCollider new_cc = to.AddComponent<CapsuleCollider>();
                new_cc.radius = cc.radius;
                new_cc.height = cc.height;
                new_cc.center = cc.center;
                Destroy(cc);
            }
        }
        return to;
    }

}
