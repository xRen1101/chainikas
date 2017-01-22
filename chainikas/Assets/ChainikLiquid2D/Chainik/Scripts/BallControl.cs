using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

    public GameObject air;

    private bool movingUp = false;
    private Rigidbody2D rig;
    private float gravityScale = 0f;
    bool dropping = false;
    bool airAdded = false;

	// Use this for initialization
	void Start ()
    {
        rig = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        if ((transform.position.y >= 0.1f) && movingUp)
        {
            rig.gravityScale = 0;
            //rig.inertia = 0; //FunStuff, things will break
            rig.velocity = Vector2.zero;
            transform.position = new Vector3 (transform.position.x ,0.1f, transform.position.z);
            movingUp = false;
        }

        //ANdroid
        if (Input.touchSupported)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                rig.gravityScale = 1;
                dropping = true;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                rig.gravityScale = -1;
                movingUp = true;
                dropping = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("DropIt"))
            {
                rig.gravityScale = 1;
                dropping = true;
            }
            if (Input.GetButtonUp("DropIt"))
            {
                rig.gravityScale = -1;
                movingUp = true;
                dropping = false;
            }
        }

        if (dropping)
        {
            rig.gravityScale += 0.1f;
        }
        if (transform.position.y <= -3.5f && !airAdded)
        {
            airAdded = true;
            air.transform.position += new Vector3(0, -0.05f, 0);
        }
        if (transform.position.y >= -3.5f)
            airAdded = false;
    }
}
