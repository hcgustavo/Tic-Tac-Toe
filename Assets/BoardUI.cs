using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoardUI : MonoBehaviour {
    public GameObject boardPos00;
    public GameObject boardPos01;
    public GameObject boardPos02;
    public GameObject boardPos10;
    public GameObject boardPos11;
    public GameObject boardPos12;
    public GameObject boardPos20;
    public GameObject boardPos21;
    public GameObject boardPos22;

    private Text[,] boardPosText;

    // Use this for initialization
    void Start () {
        boardPosText = new Text[3, 3];
        boardPosText[0, 0] = boardPos00.GetComponent<Text>();
        boardPosText[0, 1] = boardPos01.GetComponent<Text>();
        boardPosText[0, 2] = boardPos02.GetComponent<Text>();
        boardPosText[1, 0] = boardPos10.GetComponent<Text>();
        boardPosText[1, 1] = boardPos11.GetComponent<Text>();
        boardPosText[1, 2] = boardPos12.GetComponent<Text>();
        boardPosText[2, 0] = boardPos20.GetComponent<Text>();
        boardPosText[2, 1] = boardPos21.GetComponent<Text>();
        boardPosText[2, 2] = boardPos22.GetComponent<Text>();
    }
	

    public void UpdateBoard(Move m, Board board, int currentPlayer)
    {
        if(currentPlayer == Board.PLAYER_X)
        {
            boardPosText[m.GetRow(), m.GetColumn()].text = "X";
        }
        else
        {
            boardPosText[m.GetRow(), m.GetColumn()].text = "O";
        }
    }

    public void Clear()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                boardPosText[i, j].text = "";
            }
        }
    }
}
