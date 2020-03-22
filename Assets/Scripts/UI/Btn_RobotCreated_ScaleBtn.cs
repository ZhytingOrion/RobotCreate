using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Btn_RobotCreated_ScaleBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int LessOrMore = 1;
    public Area_RobotCreated_Control controlArea;

    private bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed = controlArea.speedScale;
        GameObject obj = controlArea.root.cntControlObj;
        if (isClicked)
        {
            if (obj != null)
                obj.transform.localScale += speed * controlArea.scaleDir * LessOrMore * Time.deltaTime;
            else
                controlArea.root.ShowTip();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        isClicked = true;
        GameObject obj = controlArea.root.cntControlObj;
        if (obj != null && controlArea.speedScale > 0)
            controlArea.root.withdrawBtn.AddOp(controlArea.root.cntControlObj, RobotCreated_Operation_Name.Resize);
    }

    public void OnPointerUp(PointerEventData data)
    {
        isClicked = false;
        GameObject obj = controlArea.root.cntControlObj;
        if (obj != null && controlArea.speedScale > 0)
            controlArea.root.withdrawBtn.ModifyOp(controlArea.root.cntControlObj, RobotCreated_Operation_Name.Resize);
    }
}
