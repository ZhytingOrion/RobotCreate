using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Plane_RobotCreate : MonoBehaviour {

    public GameObject robot_create = null;
    public GameObject cntControlObj = null;
    public int cntControlObjIndex = -1;
    public List<Btn_RobotCreated_Choosen> ChoosenBtns = new List<Btn_RobotCreated_Choosen>();
    public GameObject ChoosenBtnTap = null;
    public GameObject Tip;
    public Material outlineMat;

    //暂用
    [HideInInspector]
    private string newObjName;
    public InputField inputFiled;

    public Action cntControlObjChange;

	// Use this for initialization
	void Start () {
        //CreateChoosenBtn("Cola Can");
        //CreateChoosenBtn("binoculars");
        //CreateChoosenBtn("bin");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Submit()
    {
        RobotCreated robot = new RobotCreated();
        if (robot_create == null)
            robot_create = GameObject.Find("Robot");
        for(int i = 0; i<robot_create.transform.childCount; i++)
        {
            GameObject child = robot_create.transform.GetChild(i).gameObject;
            RobotCreatedComponent comp = new RobotCreatedComponent();
            comp.prefabName = child.name.Replace("(Clone)", "");
            comp.pos = child.transform.localPosition;
            comp.rot = child.transform.localRotation.eulerAngles;
            comp.scale = child.transform.localScale;
            robot.RobotComponents.Add(comp);
        }
        RobotCreateTool.Instance.SaveJsonRobotCreated(robot);
        ShowSuccessSubmitTip();
    }

    public void setCntControlObj(GameObject obj, int index)
    {
        if(this.cntControlObj!=null)
        {
            GameObject outline = this.cntControlObj.transform.Find("Outline").gameObject;
            if(outline!=null)
                outline.SetActive(false);
        }

        if(this.cntControlObjIndex!=-1 && this.cntControlObjIndex<ChoosenBtns.Count)
            ChoosenBtns[this.cntControlObjIndex].Recover();

        this.cntControlObj = obj;
        this.cntControlObjIndex = index;

        GameObject outlineObj = this.cntControlObj.transform.Find("Outline").gameObject;
        if(outlineObj!=null)
            outlineObj.SetActive(true);

        this.cntControlObjChange();

        //for(int i = 0; i<ChoosenBtns.Count; ++i)
        //{
        //    if (i == index) continue;
        //    ChoosenBtns[i].Recover();
        //}
    }

    public void DeleteObj()
    {
        if(this.cntControlObjIndex != -1 && this.cntControlObj != null)
        {
            Destroy(ChoosenBtns[this.cntControlObjIndex].gameObject);
            ChoosenBtns.RemoveAt(this.cntControlObjIndex);
            RerangeChoosenBtn();
        }
    }

    public Btn_RobotCreated_Choosen CreateChoosenBtn(string objName)
    {                
        //Create Obj
        GameObject myObj = Instantiate(Resources.Load<GameObject>("RobotComponents/" + objName));
        myObj.transform.parent = robot_create.transform;
        myObj.transform.localPosition = Vector3.zero;

        //Set Outline
        GameObject outlineObj = Instantiate(Resources.Load<GameObject>("RobotComponents/" + objName));
        outlineObj.name = "Outline";
        outlineObj.transform.parent = myObj.transform;
        outlineObj.transform.localPosition = Vector3.zero;
        if (outlineObj.GetComponent<Renderer>() !=null)
        {
            Material[] mats = outlineObj.GetComponent<Renderer>().sharedMaterials;
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = outlineMat;
            }
            outlineObj.GetComponent<Renderer>().sharedMaterials = mats;
        }
        foreach(Renderer r in outlineObj.GetComponentsInChildren<Renderer>())
        {
            Material[] mats = r.sharedMaterials;
            for(int i = 0; i<mats.Length; i++)
            {
                mats[i] = outlineMat;
            }
            r.sharedMaterials = mats;
        }
        outlineObj.SetActive(false);

        //Create Choose Btn
        GameObject choosenBtn = Instantiate(Resources.Load<GameObject>("UI/ChooseButton"), ChoosenBtnTap.transform);
        Sprite tex = Resources.Load<Sprite>("RobotComponents/UI/Thumbnail_" + objName);
        Btn_RobotCreated_Choosen btn = choosenBtn.GetComponent<Btn_RobotCreated_Choosen>();
        btn.Inst(myObj, ChoosenBtns.Count, tex, this);
        ChoosenBtns.Add(btn);

        RerangeChoosenBtn();
        return btn;
    }

    //暂用
    public void CreateObj()
    {
        CreateChoosenBtn(newObjName);
    }

    //暂用
    public void inputObjName()
    {
        Debug.Log("Get new input text: " + inputFiled.text);
        newObjName = inputFiled.text;
    }

    public void RerangeChoosenBtn()
    {
        int number = ChoosenBtns.Count;
        float middle = (number - 1) * 0.5f;
        for(int i = 0; i<number; i++)
        {
            Vector3 pos = ChoosenBtns[i].transform.localPosition;
            ChoosenBtns[i].transform.localPosition = new Vector3((i - middle) * 180.0f, pos.y, pos.z);
            ChoosenBtns[i].index = i;
        }
    }

    public void ShowTip()
    {
        if (Tip.activeInHierarchy) return;
        Tip.SetActive(true);
        Tip.transform.Find("Text").GetComponent<Text>().text = "请先选择要控制的物体！";
        StartCoroutine(HideTipDelay(Tip, 1.0f));
    }

    public IEnumerator HideTipDelay(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

    public void ShowSuccessSubmitTip()
    {
        StopAllCoroutines();
        Tip.SetActive(true);
        Tip.transform.Find("Text").GetComponent<Text>().text = "提交成功！给你个抱抱！暂时没有查看已完成机器人的方法...";
        StartCoroutine(HideTipDelay(Tip, 3.0f));
    }
}
