using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btn_RobotCreated_ControlAreaBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public Area_RobotCreated_Control controlArea;
    public bool isBtnShow = false;
    private Vector3 normalSize = new Vector3(0.8f, 0.8f, 0.8f);
    private Vector3 highlightSize = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.localPosition = new Vector3(50.0f, this.transform.localPosition.y, this.transform.localPosition.z);
        this.transform.localScale = normalSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData data)
    {
        //this.transform.localPosition = new Vector3(0.0f, this.transform.localPosition.y, this.transform.localPosition.z);
        this.transform.localScale = highlightSize;
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!this.isBtnShow)
        {
            //this.transform.localPosition = new Vector3(50.0f, this.transform.localPosition.y, this.transform.localPosition.z);

            this.transform.localScale = normalSize;
        }
    }

    public void OnClick()
    {
        this.isBtnShow = !this.isBtnShow;
        isBtnActive(this.isBtnShow);
        controlArea.OnBtnClicked(this.index);
    }

    public void isBtnActive(bool isActive)
    {
        if (isActive)
        {
            //this.transform.localPosition = new Vector3(0.0f, this.transform.localPosition.y, this.transform.localPosition.z);
            this.transform.localScale = highlightSize;
        }
        else
        {
           // this.transform.localPosition = new Vector3(50.0f, this.transform.localPosition.y, this.transform.localPosition.z);
            this.transform.localScale = normalSize;
        }
        this.isBtnShow = isActive;
    }
}
