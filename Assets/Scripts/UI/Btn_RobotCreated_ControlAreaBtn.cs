using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btn_RobotCreated_ControlAreaBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public Area_RobotCreated_Control controlArea;
    public bool isBtnShow = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(50.0f, this.transform.localPosition.y, this.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData data)
    {
        this.transform.localPosition = new Vector3(0.0f, this.transform.localPosition.y, this.transform.localPosition.z);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!this.isBtnShow)
        {
            this.transform.localPosition = new Vector3(50.0f, this.transform.localPosition.y, this.transform.localPosition.z);

        }
    }

    public void OnClick()
    {
        controlArea.OnBtnClicked(this.index);
        this.isBtnShow = !this.isBtnShow;
        isBtnActive(this.isBtnShow);
    }

    public void isBtnActive(bool isActive)
    {
        if (isActive)
        {
            this.transform.localPosition = new Vector3(0.0f, this.transform.localPosition.y, this.transform.localPosition.z);
        }
        else
        {
            this.transform.localPosition = new Vector3(50.0f, this.transform.localPosition.y, this.transform.localPosition.z);
        }
        this.isBtnShow = isActive;
    }
}
