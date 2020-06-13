using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject buttonRed;
    public GameObject buttonYellow;
    public GameObject buttonGreen;
    public GameObject buttonBlue;

    public GameObject pawn;
    public GameObject menu;
    public GameObject textViewOnGame;
    private int countPlayers;

    public GameObject endMenu;
    public Text textWin;

    public GameObject rotationCam;
    //public int indexMoveNow;

    //public string tempPlayer;
    //public int countDice;
    // Use this for initialization
    GameManager gameManager;
    bool startGame;

    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        countPlayers = 1;
        startGame = false;
        GetField("Red");
        endMenu.SetActive(false);
        buttonBlue.SetActive(true);
        buttonYellow.SetActive(false);
        buttonGreen.SetActive(false);
        buttonBlue.SetActive(false);

        textViewOnGame.SetActive(false);
        menu.SetActive(true);
    }
    private void Update()
    {
        if (!gameManager.isGameActiv)
        {
            rotationCam.transform.Rotate(new Vector3(0, 0.5f, 0));
            startGame = true;
        }
        else if(startGame) 
        {
            rotationCam.transform.rotation = Quaternion.Euler(0, 0, 0);
            startGame = false;
        }
        if (gameManager.isGameActiv)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (rotationCam.transform.position.z >= 0)
                {
                    rotationCam.transform.position += new Vector3(0, 0, -0.5f);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (rotationCam.transform.position.z <= 13)
                {
                    rotationCam.transform.position += new Vector3(0, 0, 0.5f);
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotationCam.transform.Rotate(new Vector3(0, 0.5f, 0));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rotationCam.transform.Rotate(new Vector3(0, -0.5f, 0));
            }
        }
    }
    public void ButtonStart()
    {
        if (gameManager.listOrder.Count >= 2)
        {
            menu.SetActive(false);
            textViewOnGame.SetActive(true);
            Order();
            MakePawns();
            gameManager.isGameActiv = true;
            gameManager.MoveNow();
        }
    }
    public void ButtonRandomOrder()
    {
        Text textButton = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<Text>();
        Debug.Log(textButton.text);
        string tempName ="";
        if (textButton.text == "Losuj kolejność")
        {
            if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '1')
            {
                tempName = "Red";
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '2')
            {
                tempName = "Yellow";
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '3')
            {
                tempName = "Green";
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '4')
            {
                tempName = "Blue";
            }
            
            foreach (Player player in gameManager.listOrder.ToArray())
            {
                if (player.name.ToCharArray()[0] == tempName.ToCharArray()[0])
                {
                    int count = gameManager.ThrowDice();
                    player.numerField = count;
                    textButton.text = count.ToString();
                }
            }
            //textButton.text = "Usun Gracza";
        }
       
    }
    public void ButtonAddPlayer()
    {
        Text textButton = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<Text>();
        
        if (textButton.text == "Dodaj Gracza")
        {
            if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '1')
            {
                GetField("Red");
                buttonRed.SetActive(true);
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '2')
            {
                GetField("Yellow");
                buttonYellow.SetActive(true);
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '3')
            {
                GetField("Green");
                buttonGreen.SetActive(true);
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '4')
            {
                GetField("Blue");
                buttonBlue.SetActive(true);
            }
            textButton.text = "Usun Gracza";
        }
        else
        {
            if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '1')
            {
                DeleteField("Red");
                buttonRed.SetActive(false);
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '2')
            {
                DeleteField("Yellow");
                buttonYellow.SetActive(false);
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '3')
            {
                DeleteField("Green");
                buttonGreen.SetActive(false);
            }
            else if (EventSystem.current.currentSelectedGameObject.name.ToCharArray()[EventSystem.current.currentSelectedGameObject.name.Length - 1] == '4')
            {
                DeleteField("Blue");
                buttonBlue.SetActive(false);
            }
            textButton.text = "Dodaj Gracza";
        }
        
    }

    void GetField(string color)
    {
        Debug.Log("dodwanie gracza");
        gameManager.listPlayers.Add(new Player() { name = color + "_1", numerField = 0 });
        gameManager.listPlayers.Add(new Player() { name = color + "_2", numerField = 0 });
        gameManager.listPlayers.Add(new Player() { name = color + "_3", numerField = 0 });
        gameManager.listPlayers.Add(new Player() { name = color + "_4", numerField = 0 });
        gameManager.listOrder.Add(new Player() { name = color, numerField = 1 });
        countPlayers++;
    }
    void Order()
    {
        int x = 1;
        for(int i=1; i <= 6; i++)
        {
            foreach (Player player in gameManager.listOrder)
            {
                Debug.Log("i:" + i + " playr:" + player.numerField);
                if (player.numerField == i && x <= gameManager.listOrder.Count)
                {
                    player.numerField = x;
                    //Debug.Log("x:" + x);
                    x++;
                }
            }
        }
        
    }
    void DeleteField(string color)
    {
        Debug.Log("usuwanie gracza");
        foreach(Player player in gameManager.listPlayers.ToArray())
        {
            if (player.name.ToCharArray()[0] == color.ToCharArray()[0])
            {
                gameManager.listPlayers.Remove(player);
            }
        }
        foreach (Player player in gameManager.listOrder.ToArray())
        {
            if (player.name.ToCharArray()[0] == color.ToCharArray()[0])
            {
                gameManager.listOrder.Remove(player);
            }
        }
        countPlayers--;
    }
    void MakePawns()
    {
        foreach (Player player in gameManager.listPlayers)
        {
            GameObject prefab = (GameObject)Instantiate(pawn);
            prefab.name = player.name;
            if (player.name.ToCharArray()[0] == 'R')
            {
                prefab.GetComponent<Renderer>().material.color = Color.red;

            }
            else if (player.name.ToCharArray()[0] == 'Y')
            {
                prefab.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (player.name.ToCharArray()[0] == 'G')
            {
                prefab.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (player.name.ToCharArray()[0] == 'B')
            {
                prefab.GetComponent<Renderer>().material.color = Color.blue;
            }
            prefab.transform.position = GameObject.Find("Pola_Start/S_" + player.name.ToCharArray()[0] + "_" + player.name.ToCharArray()[player.name.Length - 1]).transform.position;
            prefab.transform.position = new Vector3(prefab.transform.position.x, 1.1f, prefab.transform.position.z);
        }
    }
    public void ButtonNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowWinner(string name)
    {
        textWin.text = "Wygrał gracz:\n "+ name;
        endMenu.SetActive(true);
    }

        
}
