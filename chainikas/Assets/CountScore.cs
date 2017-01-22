using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountScore : MonoBehaviour {

    public int totalScore = 0;
    public bool gameEnded = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameEnded)
        {
            if (totalScore > 30)
            {
                gameEnded = true;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DynamicParticle(Clone)")
        {
            totalScore++;
            DynamicParticle dynPar = collision.GetComponent<DynamicParticle>();
            if(dynPar) dynPar.isCounted = true;
        }
    }
}
