using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour {

    // Use this for initialization

    GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

    }
    void OnMouseDown()
    {
        if (gameManager.isGameActiv)
        {
            if(gameManager.currentlyPawn != null)
            {
                if ((gameManager.currentlyPawn.name != transform.name) && (gameManager.currentlyPawn.name.ToCharArray()[0] == transform.name.ToCharArray()[0]) )
                {
                    Debug.Log("zmiana ponka");
                    gameManager.canMove = !gameManager.canMove;
                    gameManager.selectField = !gameManager.selectField;
                }
            }
            if (gameManager.countDice != -1 && gameManager.canMove)
            {
                Debug.Log("klik pionek");
                gameManager.CheckPosition(transform.name);
            }
        }
        
        
        
    }
    private void Update()
    {
        if (gameManager.selectField)
        {
            if(transform.name.ToCharArray()[0] != gameManager.tempPlayer.ToCharArray()[0])
            {
                transform.GetComponent<MeshCollider>().enabled = false;
            }
        }
        else
        {
            transform.GetComponent<MeshCollider>().enabled = true;
        }
    }




}
