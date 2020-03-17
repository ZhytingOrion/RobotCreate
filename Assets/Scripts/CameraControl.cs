using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
        this.transform.position = target.transform.position + new Vector3(1.0f, 0.5f, 0);
	}

    private void LateUpdate()
    {
        this.transform.position = target.transform.position + new Vector3(1.0f, 0.5f, 0);
    }
}
