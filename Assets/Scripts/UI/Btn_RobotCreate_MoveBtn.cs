using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btn_RobotCreate_MoveBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 dir;
    public Area_RobotCreated_Control controlArea;

    private bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = controlArea.speed;
        GameObject obj = controlArea.root.cntControlObj;
        if (isClicked)
        {
            if (obj != null)
                obj.transform.localPosition += speed * dir * Time.deltaTime;
            else
                controlArea.root.ShowTip();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        isClicked = true;
        GameObject obj = controlArea.root.cntControlObj;
        if (obj != null && controlArea.speed > 0)
            controlArea.root.withdrawBtn.AddOp(controlArea.root.cntControlObj, RobotCreated_Operation_Name.Move);
    }

    public void OnPointerUp(PointerEventData data)
    {
        isClicked = false;
        GameObject obj = controlArea.root.cntControlObj;
        if (obj != null && controlArea.speed > 0)
            controlArea.root.withdrawBtn.ModifyOp(controlArea.root.cntControlObj,RobotCreated_Operation_Name.Move);
    }
}
