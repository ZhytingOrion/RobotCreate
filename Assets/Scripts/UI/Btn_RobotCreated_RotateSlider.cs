using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btn_RobotCreated_RotateSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Area_RobotCreated_Control controlArea;

    public void OnPointerDown(PointerEventData data)
    {
        GameObject obj = controlArea.root.cntControlObj;
        if (obj != null && controlArea.speedScale > 0)
            controlArea.root.withdrawBtn.AddOp(controlArea.root.cntControlObj, RobotCreated_Operation_Name.Rotate);
    }

    public void OnPointerUp(PointerEventData data)
    {
        GameObject obj = controlArea.root.cntControlObj;
        if (obj != null && controlArea.speedScale > 0)
            controlArea.root.withdrawBtn.ModifyOp(controlArea.root.cntControlObj, RobotCreated_Operation_Name.Rotate);
    }
}
