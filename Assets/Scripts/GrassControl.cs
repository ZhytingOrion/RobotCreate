using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Material mat = this.GetComponent<Renderer>().material;
        Vector3 pos = this.transform.position;
        Vector4 value = new Vector4(pos.x, pos.y, pos.z, 1);
        mat.SetVector("_ObjPos", value);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
