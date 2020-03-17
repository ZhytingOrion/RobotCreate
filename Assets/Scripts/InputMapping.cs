using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapping{

    public static InputMapping Instance
    {
        get
        {
            return _instance;
        }
    }
    private static InputMapping _instance;

    void Awake()
    {
        _instance = this;
    }

    //public KeyCode GetKeyCode()
}
