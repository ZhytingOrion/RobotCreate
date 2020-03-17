using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasType
{
    Tip_Fall,
}


public class GameControl : MonoBehaviour {

    public static GameControl Instance;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadCanvas(CanvasType type)
    {
        GameObject root_Canvas = GameObject.Find("Canvas");
        GameObject canvas = Instantiate(Resources.Load<GameObject>("UI/" + type.ToString()));
        canvas.transform.parent = root_Canvas.transform;
        canvas.transform.localPosition = Vector3.zero;
    }
}
