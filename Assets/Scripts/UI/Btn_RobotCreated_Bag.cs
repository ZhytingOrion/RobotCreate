using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_RobotCreated_Bag : MonoBehaviour
{

    public GameObject Bag;

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
        if (Bag.activeInHierarchy)
            Bag.SetActive(false);
        else
            Bag.SetActive(true);
    }
}
