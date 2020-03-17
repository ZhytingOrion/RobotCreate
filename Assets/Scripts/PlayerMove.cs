using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Transform footLoc;
    public float speed = 1.0f;
    public float jump = 1.0f;

	// Use this for initialization
	void Start () {
        if (footLoc == null)
            footLoc = this.transform.Find("footLoc").transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += MoveControl();
        if (Input.GetKeyDown(KeyCode.Space))
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jump, ForceMode.Impulse);
        SetFootLoc();
	}

    Vector3 MoveControl()
    {
        Vector3 dir = Vector3.zero;
        float dirRatio = Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            dir += Vector3.right * dirRatio;
        if(Input.GetKey(KeyCode.W))
            dir += Vector3.left * dirRatio;
        if (Input.GetKey(KeyCode.D))
            dir += Vector3.forward * dirRatio;
        if (Input.GetKey(KeyCode.A))
            dir += Vector3.back * dirRatio;
        return dir;
    }

    void SetFootLoc()
    {
        Vector4 value = new Vector4(footLoc.transform.position.x, footLoc.transform.position.y, footLoc.transform.position.z, 1);
        Shader.SetGlobalVector("_CharacterPos", value);
    }
}
