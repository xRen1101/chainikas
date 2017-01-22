using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowScreen(bool show)
    {
        gameObject.SetActive(show);
    }
}
