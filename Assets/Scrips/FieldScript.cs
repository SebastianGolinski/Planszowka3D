using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour {
    GameManager gameManager;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        if (gameManager.isGameActiv && gameManager.countDice != -1 && gameManager.selectField)
        {
            gameManager.TransformPosition(transform);
        }
        
    }
}
