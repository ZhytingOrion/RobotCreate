using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RobotEmoji
{
    Normal,
    Angry,
    Happy,
    Cry,
    MaxNum    //use to get the number of the emoji
}

public class RobotController : MonoBehaviour {

    public float TurnSpeed = 90.0f;
    public float WalkSpeed = 1.0f;
    public float JumpSpeed = 10.0f;
    public float DelayJumpTime = 0.1f;
    public float ChangingFaceTime = 1.0f;

    bool flag_isWalking = false;
    bool flag_isJumping = false;
    bool flag_isChangingFace = false;
    bool flag_isFallingtoGround = false;

    Material m_FaceMat;

    [HideInInspector]
    public RobotEmoji Emoji = RobotEmoji.Normal;  //Robot face emoji

	// Use this for initialization
	void Start () {
        m_FaceMat =  this.transform.Find("Face").GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        if (flag_isFallingtoGround) return;   //Falling, can not move

        //walking
        bool new_isWalking = isWalking();
        if (new_isWalking != flag_isWalking)
        {
            this.GetComponent<Animator>().SetBool("isWalk", new_isWalking);
            flag_isWalking = new_isWalking;
        }
        if (new_isWalking)
        {
            this.transform.position += this.transform.right * WalkSpeed * Time.deltaTime;
        }

        //turnAround
        //turnAround();

        //Jump
        if(!flag_isJumping && isJumping())
        {
            flag_isJumping = true;
            StartCoroutine(ReadyJump(DelayJumpTime));
            this.GetComponent<Animator>().SetBool("isJump", true);
        }

        //ChangingFace
        ChangingFaceInput();

        //FalltoGround
        if (isFalltoGround())
        {
            ChangingFaceSpecific(RobotEmoji.Cry);
            flag_isFallingtoGround = true;
            GameControl.Instance.LoadCanvas(CanvasType.Tip_Fall);
        }
	}

    //Getting Input of Walking
    bool isWalking()
    {
        bool isWalk = false;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            isWalk = true;
        }
        if (turnAround()) isWalk = true;

        return isWalk;
    }

    //Getting Input of Turning
    bool turnAround()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up, -TurnSpeed * Time.deltaTime);
            return true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime);
            return true;

        }
        return false;
    }
    
    //Getting Input of Jumping
    bool isJumping()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    //Judge if it falls
    bool isFalltoGround()
    {
        float Angle = Vector3.Angle(this.transform.up, Vector3.up);
        if (Mathf.Abs(Angle) > 45.0f)
            return true;
        else return false;
    }

    public void ResetRobot()
    {
        this.transform.position = this.transform.position + Vector3.up * 3;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        flag_isFallingtoGround = false;
    }

    //Collision Infos
    private void OnCollisionEnter(Collision collision)
    {
        //Judge when to fall on the ground
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (flag_isJumping)
            {
                flag_isJumping = false;
                this.GetComponent<Animator>().SetBool("isJump", false);
            }
        }
    }

    //Delay the Jump Anim, to Match the Anim
    IEnumerator ReadyJump(float time)
    {
        yield return new WaitForSeconds(time);
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
    }

    //Getting Input of Changing Face
    void ChangingFaceInput()
    {
        if (flag_isChangingFace) return;
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ChangingFace());
        }
    }

    void ChangingFaceSpecific(RobotEmoji _emoji)
    {
        int number = (int)RobotEmoji.MaxNum;
        int index = _emoji - Emoji;
        if (_emoji < Emoji)
            index += number;
        StartCoroutine(ChangingFace(index));
    }

    //Changing face
    IEnumerator ChangingFace(int index = 1)
    {
        flag_isChangingFace = true;
        Emoji = (RobotEmoji)((int)(Emoji + index) % (int)RobotEmoji.MaxNum);
        Vector2 offset = m_FaceMat.GetTextureOffset("_MainTex");
        Vector2 origin = offset;
        Vector2 new_offset = offset + new Vector2(10f + index * 0.25f, 0.0f);
        for(float t = 0.0f; t<=ChangingFaceTime; t+=Time.deltaTime)
        {
            offset = Vector2.Lerp(origin, new_offset, t / ChangingFaceTime);
            m_FaceMat.SetTextureOffset("_MainTex", offset);
            yield return null;
        }
        offset = new Vector2(new_offset.x % 1, new_offset.y % 1);
        m_FaceMat.SetTextureOffset("_MainTex", offset);
        flag_isChangingFace = false;
    }
}
