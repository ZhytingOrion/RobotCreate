using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Btn_RobotCreated_Choosen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public int index = -1;
    [HideInInspector]
    public GameObject myObj = null;
    [HideInInspector]
    public Sprite objTex = null;
    [HideInInspector]
    public int FloorLevel = 0;
    [HideInInspector]
    public float height;

    public Plane_RobotCreate planeRoot = null;
    public Sprite highLightTex = null;
    public Sprite normalTex = null;
    public Image objTexComp = null;


    public void Awake()
    {
        if (objTexComp == null)
            objTexComp = this.transform.Find("ObjImage").GetComponent<Image>();
        height = this.GetComponent<RectTransform>().sizeDelta.y * 0.5f;
    }

    public void Inst(GameObject obj, int index, Sprite tex, Plane_RobotCreate root)
    {
        this.index = index;
        myObj = obj;
        planeRoot = root;
        objTex = tex;
        objTexComp.sprite = tex;
    }

    public IEnumerator flowAnim(float toLocalY)
    {
        Vector3 pos = this.transform.localPosition;
        float originY = pos.y;
        for(float t = 0.0f; t<0.1f; t+=Time.deltaTime)
        {
            pos = this.transform.localPosition;
            this.transform.localPosition = new Vector3(pos.x, Mathf.Lerp(originY, toLocalY, t * 10f), pos.z);
            yield return null;
        }
        pos = this.transform.localPosition;
        this.transform.localPosition = new Vector3(pos.x, toLocalY, pos.z);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        StartCoroutine(flowAnim(0.0f + FloorLevel * height));
    }

    public void OnPointerExit(PointerEventData data)
    {
        StartCoroutine(flowAnim(-30.0f + FloorLevel * height));
    }

    public void BeChoosen()
    {
        planeRoot.setCntControlObj(myObj, index);
        this.GetComponent<Image>().sprite = highLightTex;
    }
    
    public void Recover()
    {
        this.GetComponent<Image>().sprite = normalTex;
    }

    private void OnDestroy()
    {
        Destroy(myObj);
    }
}
