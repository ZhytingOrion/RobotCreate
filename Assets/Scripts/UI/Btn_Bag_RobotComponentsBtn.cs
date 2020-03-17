using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Bag_RobotComponentsBtn : MonoBehaviour
{

    public Plane_RobotCreate root;
    public string compName;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        root.CreateChoosenBtn(compName);
    }
}
