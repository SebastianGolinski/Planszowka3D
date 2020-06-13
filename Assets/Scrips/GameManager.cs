using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<Player> listPlayers = new List<Player>();  //list wszystkich pionków w grze
    public List<Player> listOrder = new List<Player>();     // lista kolejnosci graczy
    public List<int> listCheckField = new List<int>();
    public int countDice;       // liczba wylosowanych oczek
    public bool isGameActiv;    //czy gra rozpoczęta
    public string tempPlayer;    //obecy kolor/gracz nazwa
    public GameObject currentlyPawn;
    public Text textView;
    private int indexMoveNow;  //kto idzie teraz
    public bool selectField;

    List<int> indexFieldList = new List<int>();
    List<int> indexFieldListToIf = new List<int>();
    List<FieldValue> saveColor = new List<FieldValue>();
    Board board;
    GameObject temp;
    GameObject tempMove;
    public int countMove;
    int currentlyCountMove;
    public bool mustDo;
    public bool canMove;
    bool canThrow;
    string tempName;
    int lastCountDice;
    int countLocationEnd;
    MenuManager menuManager;
    private void Awake()
    {
        isGameActiv = false;
        indexMoveNow = 1;
    }
    void Start () {
        countLocationEnd = 11;
        canThrow = true;
        selectField = false;
        canMove = false;
        mustDo = true;
        menuManager = GameObject.Find("Manager").GetComponent<MenuManager>();
        board = GameObject.Find("Manager").GetComponent<Board>();
        countDice = 0;
        lastCountDice = 0;


    }

    public void ButtonThrow()
    {
        if (lastCountDice == 6 && countDice != -2 && !mustDo)
        {
            canThrow = true;
            mustDo = true;
            canMove = true;
            countDice = -1;
        }
        if (countDice == -1 || countDice == 6 && canThrow)
        {
            countDice = ThrowDice();
            lastCountDice = countDice;
            textView.text = "Ruch: " + tempPlayer + "\nRzuciłeś: " + countDice;
            if (firstPawn())
            {
                canMove = true;
                canThrow = false;
                //mustDo = false;
            }
        }
    }

    private void Update()
    {
        textView.text = "Ruch: " + tempPlayer + "\nRzuciłeś: " + countDice;

    }
    public void ButtonEnd()
    {
        if (!CheckIfMove())
        {
            Debug.Log("END");
            canThrow = true;
            mustDo = true;
            canMove = true;
            MoveNow();
        }
        indexFieldList.Clear();
        listCheckField.Clear();
        
    }

    public void MoveNow()
    {
        //Debug.Log("moveNow");
        countDice = -1;
        foreach (Player player in listOrder)
        {
            if(indexMoveNow == player.numerField)
            {
                tempPlayer = player.name;
                textView.text = "Ruch: " + tempPlayer;
                //Debug.Log("pl:" + player.name + " text:" + tempPlayer);
                indexMoveNow++;
                if(indexMoveNow > listOrder.Count)
                {
                    indexMoveNow = 1;
                }
                break;
            }
        }
    }

    bool CheckIfMove()
    {
        indexFieldListToIf.Clear();
        indexFieldList.Clear();
        listCheckField.Clear();
        Debug.Log("checkifMove");
        countMove = countDice;
        if (lastCountDice == 6 && countDice != -2 && !mustDo)
        {
            canThrow = true;
            mustDo = true;
            canMove = true;
            countDice = -1;
            return true;
        }
        if (countDice == 0 || countDice == -2)
        {
            return false;
        }
        foreach (Player pawn in listPlayers)
        {
            if(pawn.name.ToCharArray()[0] == tempPlayer.ToCharArray()[0])
            {
                if ((countDice == 1 || countDice == 6) && pawn.numerField == 0)
                {
                    if (tempPlayer.ToCharArray()[0] == 'R' && CanLeave(pawn, 2))
                    {
                        return true;
                    }
                    else if (tempPlayer.ToCharArray()[0] == 'Y' && CanLeave(pawn, 7))
                    {
                        return true;
                    }
                    else if (tempPlayer.ToCharArray()[0] == 'G' && CanLeave(pawn, 14))
                    {
                        return true;
                    }
                    else if (tempPlayer.ToCharArray()[0] == 'B' && CanLeave(pawn, 19))
                    {
                        return true;
                    }
                    //return false;
                }
                else if(pawn.numerField != 0)
                {
                    currentlyCountMove = 0;
                    ChceckIfFindPlace(pawn.numerField);
                }
                
            }
        }
        foreach(int x in listCheckField)
        {
            Debug.Log("Field/x:" + x);
        }
        foreach (Player player in listPlayers)
        {
            foreach (int index in listCheckField)
            {
               
                if (!((player.numerField == index) && (player.name.ToCharArray()[0] == tempPlayer.ToCharArray()[0])))
                {
                    return true;
                }
            }
            //return false;

        }
        return false ;
    }
    void ChceckIfFindPlace(int indexTemp)
    {
        Debug.Log("sdadadsdddddddddddd");
        indexFieldList.Add(indexTemp);
        currentlyCountMove++;
        for (int i = 0; i < board.boardMove[indexTemp - 1].Length; i++)
        {
            if (indexFieldList == null || ChackOnList(board.boardMove[indexTemp - 1][i]))
            {
                if (GameObject.Find("P" + board.boardMove[indexTemp - 1][i] + "/Lock") != null)
                {
                    if (GameObject.Find("P" + board.boardMove[indexTemp - 1][i] + "/Lock").activeInHierarchy)
                    {
                        if (currentlyCountMove >= countMove)
                        {
                            tempMove = GameObject.Find("P" + board.boardMove[indexTemp - 1][i]);
                            if (tempMove.GetComponent<Renderer>().material.color != Color.magenta)
                            {
                                listCheckField.Add(board.boardMove[indexTemp - 1][i]);
                                //temp.GetComponent<Renderer>().material.color = Color.red;
                            }
                        }
                    }
                }
                else
                {
                    if (board.boardMove[indexTemp - 1][i] != 999)
                    {
                        if (currentlyCountMove == countMove)
                        {
                            Debug.Log("sprawdzenie zmiana koloru"+ board.boardMove[indexTemp - 1][i]);
                            //tempMove = GameObject.Find("P" + board.boardMove[indexTemp - 1][i]);
                            listCheckField.Add(board.boardMove[indexTemp - 1][i]);
                            //tempMove.GetComponent<Renderer>().material.color = Color.red;
                        }
                        else
                        {
                            ChceckIfFindPlace(board.boardMove[indexTemp - 1][i]);
                        }
                    }

                }

            }
        }
        currentlyCountMove--;
    }
    public int ThrowDice()
    {
        return UnityEngine.Random.Range(1, 7);
    }

    ////////////////////
    public void backColor()
    {
        foreach (FieldValue field in saveColor)
        {
            GameObject.Find("P" + field.numberField).GetComponent<Renderer>().material.color = field.color;
            Debug.Log("ColorChange:" + GameObject.Find("P" + field.numberField).name);
        }
        //x++;
        indexFieldList.Clear();
        saveColor.Clear();
    }
    void move(int index)
    {
        temp = GameObject.Find("P" + index);
        saveColor.Add(new FieldValue() { numberField = index, color = temp.GetComponent<Renderer>().material.color });
        temp.GetComponent<Renderer>().material.color = Color.gray;
        selectField = true;
        currentlyCountMove = 0;
        findPlace(index);
    }
    void findPlace(int indexTemp)
    {
        indexFieldList.Add(indexTemp);
        currentlyCountMove++;
        for (int i = 0; i < board.boardMove[indexTemp - 1].Length; i++)
        {
            if (indexFieldList == null || ChackOnList(board.boardMove[indexTemp - 1][i]))
            {
               if(GameObject.Find("P" + board.boardMove[indexTemp - 1][i] + "/Lock") != null)
                {
                    if (GameObject.Find("P" + board.boardMove[indexTemp - 1][i] + "/Lock").activeInHierarchy)
                    {
                        if (currentlyCountMove >= countMove)
                        {
                            tempMove = GameObject.Find("P" + board.boardMove[indexTemp - 1][i]);
                            if (tempMove.GetComponent<Renderer>().material.color != Color.magenta)
                            {
                                tempMove = GameObject.Find("P" + board.boardMove[indexTemp - 1][i]);
                                saveColor.Add(new FieldValue() { numberField = board.boardMove[indexTemp - 1][i], color = tempMove.GetComponent<Renderer>().material.color });
                                tempMove.GetComponent<Renderer>().material.color = Color.magenta;
                            }
                        }
                    }
                }
                else
                {
                    if(board.boardMove[indexTemp - 1][i] != 999)
                    {
                        if (currentlyCountMove >= countMove)
                        {
                            tempMove = GameObject.Find("P" + board.boardMove[indexTemp - 1][i]);
                            if (tempMove.GetComponent<Renderer>().material.color != Color.magenta)
                            {
                                saveColor.Add(new FieldValue() { numberField = board.boardMove[indexTemp - 1][i], color = tempMove.GetComponent<Renderer>().material.color });
                                tempMove.GetComponent<Renderer>().material.color = Color.magenta;
                            }
                        }
                        else
                        {
                            findPlace(board.boardMove[indexTemp - 1][i]);
                        }
                    }
                    
                }
                
            }
        }
        currentlyCountMove--;
    }
    private bool ChackOnList(int numer)
    {
        foreach (int list in indexFieldList)
        {
            if (numer == list)
            {
                return false;
            }
        }
        return true;
    }

    public void CheckPosition(string namePawn)
    {
        backColor();
        Debug.Log(tempPlayer);
        if (namePawn.ToCharArray()[0] == tempPlayer.ToCharArray()[0])
        {
            foreach (Player player in listPlayers)
            {
                if (player.name == namePawn)
                {
                    currentlyPawn = GameObject.Find(namePawn);
                    if (player.numerField == 0)
                    {
                        Debug.Log("start");
                        if (countDice == 1 || countDice == 6)
                        {
                            if (tempPlayer.ToCharArray()[0] == 'R' && CanLeave(player, 2))
                            {
                                player.numerField = 2;
                                LeaveStart(player, namePawn);
                            }
                            else if (tempPlayer.ToCharArray()[0] == 'Y' && CanLeave(player, 7))
                            {
                                player.numerField = 7;
                                LeaveStart(player, namePawn);
                            }
                            else if (tempPlayer.ToCharArray()[0] == 'G' && CanLeave(player, 14))
                            {
                                player.numerField = 14;
                                LeaveStart(player, namePawn);
                            }
                            else if (tempPlayer.ToCharArray()[0] == 'B' && CanLeave(player, 19))
                            {
                                player.numerField = 19;
                                LeaveStart(player, namePawn);
                            }
                            canThrow = false;

                            //countDice = -1;
                            // MoveNow();
                            // mustDo = false;
                        }
                        Debug.Log("mustdo!!!!!!!");
                        mustDo = false;
                    }
                    else
                    {
                        canThrow = true;
                        canMove = false;
                        Debug.Log("ruch");
                        countMove = countDice;
                        move(player.numerField);
                    }
                }
            }
        }
    }
    void LeaveStart(Player player, String nameField)
    {
        Debug.Log("Kurwa wyszedł");
        GameObject temp = GameObject.Find(nameField);
        temp.transform.position = GameObject.Find("P" + player.numerField).transform.position;
        temp.transform.position = new Vector3(temp.transform.position.x, 1.1f, temp.transform.position.z);
        canMove = false;
        countDice = -2;
        foreach (Player pl in listPlayers)
        {
            if (player.numerField == pl.numerField)
            {
                if (player.name.ToCharArray()[0] != pl.name.ToCharArray()[0])
                {
                    Debug.Log("usuwanie gracza");
                    DeletePawnOtherPlayer(pl);
                }
            }
        }

    }
    bool CanLeave(Player player, int indexField)
    {
        Debug.Log("spr wyjscia");
        foreach(Player pl in listPlayers)
        {
            Debug.Log(player.name.ToCharArray()[0] + "|" + pl.name.ToCharArray()[0] + "|" + player.numerField + "|" + indexField);
            if(player.name.ToCharArray()[0] == pl.name.ToCharArray()[0] && pl.numerField == indexField)
            {
                Debug.Log("false kurwa");
                return false;
            }
        }
        return true;
    }
    public void TransformPosition(Transform newLocation)
    {
        Debug.Log("transformpos");
        //mustDo = false;
        bool tranformPos = true;
        bool endPosition = false;
        foreach(Player pawn in listPlayers)
        {
            if(currentlyPawn.name == pawn.name )
            {
                string tmp = "";
                for (int i = 1; i < newLocation.name.Length; i++)
                {
                    tmp += newLocation.name.ToCharArray()[i];
                }
                if (CheckOnlist(Convert.ToInt32(tmp)))
                {
                    if (Convert.ToInt32(tmp) == 95)
                    {
                        endPosition = true;
                        tranformPos = true;
                    }
                    else
                    {
                        foreach (Player player in listPlayers)
                        {
                            if (Convert.ToInt32(tmp) == player.numerField)
                            {
                                if (tempPlayer == player.name)
                                {
                                    tranformPos = false;
                                }
                                else
                                {
                                    Debug.Log("usuwanie gracza");
                                    tranformPos = true;
                                    DeletePawnOtherPlayer(player);
                                }
                            }
                        }
                    }
                    

                    if (tranformPos)
                    {
                        Debug.Log("kurwa0");
                        selectField = false ;
                        currentlyPawn.transform.position = newLocation.position;
                        currentlyPawn.transform.position = new Vector3(currentlyPawn.transform.position.x, 1.1f, currentlyPawn.transform.position.z);
                        if (GameObject.Find(newLocation.name + "/Lock") != null)
                        {
                            Debug.Log("kurwa1");
                            if (GameObject.Find(newLocation.name + "/Lock").activeInHierarchy)
                            {
                                Debug.Log("kurwa2");
                                GameObject.Find(newLocation.name + "/Lock").SetActive(false);
                            }
                        }
                        pawn.numerField = Convert.ToInt32(tmp);
                        if (endPosition)
                        {
                            currentlyPawn.transform.position = new Vector3(countLocationEnd, 0.8f, -13);
                            countLocationEnd--;
                            checkWin();
                        }
                        
                        //MoveNow();
                        Debug.Log("mustdo!!!!!!!+++++");
                        mustDo = false;
                        backColor();
                        countDice = 0;
                    }
                    
                    //Debug.Log("temp: " + tmp);
                }
            }
        }
        
    }
    void checkWin()
    {
        int count = 0;
        foreach(Player player in listPlayers)
        {
            foreach(Player player_2 in listPlayers)
            {
                if(player.name.ToCharArray()[0] == player_2.name.ToCharArray()[0] && player_2.numerField == 95)
                {
                    count++;
                }
            }
            if(count == 4)
            {
                string winner = "";
                for(int i=0; i< player.name.Length - 2; i++)
                {
                    winner += player.name.ToCharArray()[i];
                }
                menuManager.ShowWinner(winner);
                break;
            }
            count = 0;
        }
    }
    bool CheckOnlist(int number)
    {
        foreach(FieldValue field in saveColor)
        {
            if(number == field.numberField)
            {
                return true;
            }
        }
        return false;
    }
    void DeletePawnOtherPlayer(Player player)
    {
        player.numerField = 0;
        GameObject temp = GameObject.Find(player.name);
        temp.transform.position = GameObject.Find("Pola_Start/S_" + player.name.ToCharArray()[0] + "_" + player.name.ToCharArray()[player.name.Length - 1]).transform.position;//do poprawy
        temp.transform.position = new Vector3(temp.transform.position.x, 1.1f, temp.transform.position.z);
    }
    bool firstPawn()
    {
        Debug.Log("firstpawn_1");
        foreach(Player player in listPlayers)
        {
            if(tempPlayer == player.name)
            {
                Debug.Log("firstpawn_2");
                if (player.numerField != 0)
                {
                    Debug.Log("firstpawn_3");
                    return false;
                }
            }
        }
        return true;
    }

}
public class Player
{
    public string name { get; set; }
    public int numerField { get; set; }
    //public int[] numberFieldToMove { get; set; }

}
public class FieldValue
{
    public int numberField { get; set; }
    public Color color { get; set; }

}