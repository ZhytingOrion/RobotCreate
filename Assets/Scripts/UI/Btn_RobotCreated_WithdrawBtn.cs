using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RobotCreated_Operation_Name
{
    Create,
    Move,
    Rotate,
    Resize,
    Copy,
    Delete
}

public class RobotCreated_Operation
{
    public string name;
    public Btn_RobotCreated_Choosen controlObj;  //object created after this op
    public RobotCreated_Operation_Name Op;
    public Vector3 origin_Pos;   //value before this operation
    public Quaternion origin_Rot;   //value before this operation
    public Vector3 origin_Scale;   //value before this operation
    public Vector3 to_Pos;
    public Quaternion to_Rot;
    public Vector3 to_Scale;
    public int tag = -1;   //used to recover objs; default -1, when deletesomthing, it will change to the same index;
}

public class Btn_RobotCreated_WithdrawBtn : MonoBehaviour
{
    public Plane_RobotCreate root;
    public Button RedoBtn;
    private List<RobotCreated_Operation> Operations = new List<RobotCreated_Operation>();
    private List<RobotCreated_Operation> Redo_Operations = new List<RobotCreated_Operation>();
    private int tagIndex = 0;
    public int MaxCount = 10;
    public int Redo_MaxCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().interactable = false;
        RedoBtn.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Undo()  //Undo
    {
        if (Operations.Count <= 0) return;

        RobotCreated_Operation op = Operations[Operations.Count - 1];
        switch(op.Op)
        {
            case RobotCreated_Operation_Name.Create:
                if (op.controlObj != null)
                {
                    root.DeleteObjSpecial(op.controlObj);
                }
                break;
            case RobotCreated_Operation_Name.Copy:
                if (op.controlObj != null)
                {
                    root.DeleteObjSpecial(op.controlObj);
                }
                break;
            case RobotCreated_Operation_Name.Delete:
                if (op.name != "")
                {
                    Btn_RobotCreated_Choosen choosen = root.CreateChoosenBtn(op.name);
                    choosen.myObj.transform.localPosition = op.origin_Pos;
                    choosen.myObj.transform.localRotation = op.origin_Rot;
                    choosen.myObj.transform.localScale = op.origin_Scale;

                    for(int i = 0; i<Operations.Count; i++)
                    {
                        if (Operations[i].tag == op.tag)
                            Operations[i].controlObj = choosen;
                    }
                }
                break;
            case RobotCreated_Operation_Name.Move:
                if(op.controlObj!=null && op.controlObj.myObj!=null)
                {
                    op.controlObj.myObj.transform.localPosition = op.origin_Pos;
                }
                break;
            case RobotCreated_Operation_Name.Resize:
                if (op.controlObj != null && op.controlObj.myObj != null)
                {
                    op.controlObj.myObj.transform.localScale = op.origin_Scale;
                }
                break;
            case RobotCreated_Operation_Name.Rotate:
                if (op.controlObj != null && op.controlObj.myObj != null)
                {
                    op.controlObj.myObj.transform.localRotation = op.origin_Rot;
                }
                break;
        }

        if (Redo_Operations.Count >= Redo_MaxCount) Redo_Operations.RemoveAt(0);
        Redo_Operations.Add(op);
        RedoBtn.interactable = true;

        Operations.Remove(op);

        if (Operations.Count <= 0)
            this.gameObject.GetComponent<Button>().interactable = false;
    }

    public void Redo()
    {
        if (Redo_Operations.Count <= 0) return;

        RobotCreated_Operation op = Redo_Operations[Redo_Operations.Count - 1];
        switch (op.Op)
        {
            case RobotCreated_Operation_Name.Create:
                {
                    Btn_RobotCreated_Choosen choosen = root.CreateChoosenBtn(op.name);
                    for(int i = 0; i<Redo_Operations.Count; i++)
                    {
                        if (Redo_Operations[i].tag == op.tag)
                            Redo_Operations[i].controlObj = choosen;
                    }
                }
                break;
            case RobotCreated_Operation_Name.Copy:
                {
                    Btn_RobotCreated_Choosen choosen = root.CreateChoosenBtn(op.name);
                    choosen.myObj.transform.localPosition = op.origin_Pos;
                    choosen.myObj.transform.localRotation = op.origin_Rot;
                    choosen.myObj.transform.localScale = op.origin_Scale;
                    for (int i = 0; i < Redo_Operations.Count; i++)
                    {
                        if (Redo_Operations[i].tag == op.tag)
                            Redo_Operations[i].controlObj = choosen;
                    }
                }
                break;
            case RobotCreated_Operation_Name.Delete:
                if (op.controlObj != null)
                {
                    root.DeleteObjSpecial(op.controlObj);
                }
                break;
            case RobotCreated_Operation_Name.Move:
                if (op.controlObj != null && op.controlObj.myObj != null)
                {
                    op.controlObj.myObj.transform.localPosition = op.to_Pos;
                }
                break;
            case RobotCreated_Operation_Name.Resize:
                if (op.controlObj != null && op.controlObj.myObj != null)
                {
                    op.controlObj.myObj.transform.localScale = op.to_Scale;
                }
                break;
            case RobotCreated_Operation_Name.Rotate:
                if (op.controlObj != null && op.controlObj.myObj != null)
                {
                    op.controlObj.myObj.transform.localRotation = op.to_Rot;
                }
                break;
        }

        Redo_Operations.Remove(op);
        if (Operations.Count >= MaxCount) Operations.RemoveAt(0);
        Operations.Add(op);
        this.gameObject.GetComponent<Button>().interactable = true;

        if (Redo_Operations.Count <= 0)
            RedoBtn.interactable = false;
    }

    public void AddOp(Btn_RobotCreated_Choosen choosen, RobotCreated_Operation_Name opName)
    {
        SetOpInfos(choosen, opName);
        Redo_Operations.Clear();
        RedoBtn.interactable = false;
    }

    public void AddOpDelete(Btn_RobotCreated_Choosen choosen)
    {
        Redo_Operations.Clear();
        RedoBtn.interactable = false;
        RobotCreated_Operation op = SetOpInfos(choosen, RobotCreated_Operation_Name.Delete);
        op.tag = tagIndex;
        for(int i = 0; i<Operations.Count; i++)
        {
            if (Operations[i].controlObj == op.controlObj)
                Operations[i].tag = tagIndex;
        }
        tagIndex++;
    }

    public void AddOp(GameObject obj, RobotCreated_Operation_Name opName)
    {
        SetOpInfos(obj, opName);
        Redo_Operations.Clear();
        RedoBtn.interactable = false;
    }

    public void ModifyOp(GameObject obj, RobotCreated_Operation_Name opName)
    {
        RobotCreated_Operation op = Operations.FindLast(x => x.Op == opName);
        if (op != null)
        {
            op.to_Pos = obj.transform.localPosition;
            op.to_Rot = obj.transform.localRotation;
            op.to_Scale = obj.transform.localScale;
        }
    }

    private RobotCreated_Operation SetOpInfos(Btn_RobotCreated_Choosen choosen, RobotCreated_Operation_Name opName)
    {
        if(Operations.Count >= MaxCount)
        {
            Operations.RemoveAt(0);
        }

        if (choosen != null)
        {
            this.GetComponent<Button>().interactable = true;

            GameObject obj = choosen.myObj;
            RobotCreated_Operation op = new RobotCreated_Operation();
            op.name = choosen.myObj.name.Replace("(Clone)", "");
            op.controlObj = choosen;
            op.Op = opName;
            op.origin_Pos = obj.transform.localPosition;
            op.origin_Rot = obj.transform.localRotation;
            op.origin_Scale = obj.transform.localScale;
            Operations.Add(op);
            return op;
        }
        return null;
    }

    private RobotCreated_Operation SetOpInfos(GameObject obj, RobotCreated_Operation_Name opName)
    {
        Btn_RobotCreated_Choosen choosen = root.ChoosenBtns.Find(x => x.myObj == obj);
        return SetOpInfos(choosen, opName);
    }
}
