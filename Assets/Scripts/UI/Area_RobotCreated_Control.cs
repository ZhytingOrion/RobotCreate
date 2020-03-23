using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area_RobotCreated_Control : MonoBehaviour
{
    public Plane_RobotCreate root = null;
    public Btn_RobotCreated_ControlAreaBtn moveControl;
    public Btn_RobotCreated_ControlAreaBtn RotateControl;
    public Btn_RobotCreated_ControlAreaBtn ResizeControl;
    public RawImage Image_Camera;
    private int lastActiveControlBtn = -1;

    //MoveControl
    public Slider speedSlider;
    public float speed = 1.0f;

    //RotateControl
    public Slider XSlider;
    public Slider YSlider;
    public Slider ZSlider;

    //ScaleControl
    public Slider speedSlider_Scale;
    public float speedScale = 1.0f;
    public Toggle toggle_ScaleX;
    public Toggle toggle_ScaleY;
    public Toggle toggle_ScaleZ;
    [HideInInspector]
    public Vector3 scaleDir;

    // Start is called before the first frame update
    void Start()
    {
        root.cntControlObjChange += SetObjValues;
        this.transform.localPosition = new Vector3(1280, 0, 0);
        speed = speedSlider.value;
        SetScaleDir();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        root.cntControlObjChange -= SetObjValues;
    }
    
    public void OnBtnClicked(int index) //0 move  1 rotate  2 resize  3 Copy  4 Withdraw
    {
        if(lastActiveControlBtn == -1)
        {
            ShowControlArea(true);
        }
        else
        {
            Btn_RobotCreated_ControlAreaBtn btn = GetBtnByIndex(lastActiveControlBtn);
            if (btn != null)
                btn.isBtnActive(false);
        }

        if (lastActiveControlBtn == index)
        {
            lastActiveControlBtn = -1;
            ShowControlArea(false);
        }
        else
        {
            lastActiveControlBtn = index;
            this.transform.Find("MoveControl").gameObject.SetActive(index == 0);
            this.transform.Find("RotateControl").gameObject.SetActive(index == 1);
            this.transform.Find("ResizeControl").gameObject.SetActive(index == 2);
        }
    }

    public Btn_RobotCreated_ControlAreaBtn GetBtnByIndex(int index)
    {
        switch(index)
        {
            case 0:
                return moveControl;
            case 1:
                return RotateControl;
            case 2:
                return ResizeControl;
            default:
                return null;
        }
    }

    public void ShowControlArea(bool isShow)
    {
        if (isShow)
        {
            Image_Camera.transform.localPosition = new Vector3(-400f, 0, 0);
            this.transform.localPosition = new Vector3(550, 0, 0);
        }
        else
        {
            Image_Camera.transform.localPosition = Vector3.zero;
            this.transform.localPosition = new Vector3(1280, 0, 0);
        }
    }

    //MoveControl
    public void SetSpeed()
    {
        speed = speedSlider.value;
    }

    //RotateControl
    public void RotateSliderValueChange()
    {
        GameObject obj = root.cntControlObj;
        if (obj)
            obj.transform.localRotation = Quaternion.Euler(XSlider.value, YSlider.value, ZSlider.value);
        else
            root.ShowTip();
    }

    public void SetRotateSliderValue()
    {
        if (root.cntControlObj)
        {
            Vector3 rot = root.cntControlObj.transform.localRotation.eulerAngles;
            XSlider.value = rot.x % 360.0f;
            YSlider.value = rot.y % 360.0f;
            ZSlider.value = rot.z % 360.0f;
        }
        else
            root.ShowTip();
    }


    //ScaleControl
    public void SetScaleValue()
    {

    } 

    public void SetScaleSpeed()
    {
        speedScale = speedSlider_Scale.value;
    }

    public void SetScaleDir()
    {
        scaleDir = Vector3.zero;
        if (toggle_ScaleX.isOn)
            scaleDir += Vector3.right;
        if (toggle_ScaleY.isOn)
            scaleDir += Vector3.up;
        if (toggle_ScaleZ.isOn)
            scaleDir += Vector3.forward;
    }

    //CopyControl
    public void CopyObj()
    {
        if (root.cntControlObj == null)
        {
            root.ShowTip();
        }
        else
        {
            GameObject obj = root.cntControlObj;
            Btn_RobotCreated_Choosen btn = root.CreateChoosenBtn(obj.name.Replace("(Clone)", ""));
            if (btn != null)
            {
                GameObject copyObj = btn.myObj;
                copyObj.transform.localPosition = obj.transform.localPosition;
                copyObj.transform.localRotation = obj.transform.localRotation;
                copyObj.transform.localScale = obj.transform.localScale;

                root.withdrawBtn.AddOp(btn, RobotCreated_Operation_Name.Copy);
            }
        }
    }

    public void SetObjValues()
    {
        SetRotateSliderValue();
    }

}
