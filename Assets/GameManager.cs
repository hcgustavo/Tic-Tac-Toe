using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject boardUIGO;
    public GameObject gameStatus;

    private Board board;
    private BoardUI boardUI;
    private Text gameStatusText;
    private int humanPlayer;
    private int aiPlayer;
    private bool gameStarted;
    private Minimax minimax;
    private int currentPlayer;

	// Use this for initialization
	void Start () {
        board = new Board();
        boardUI = boardUIGO.GetComponent<BoardUI>();
        gameStatusText = gameStatus.GetComponent<Text>();
        gameStarted = false;
        minimax = new Minimax();
        currentPlayer = -1;
	}

    public void SwapCurrentPlayer()
    {
        if (currentPlayer == Board.PLAYER_X)
            currentPlayer = Board.PLAYER_O;
        else
            currentPlayer = Board.PLAYER_X;
    }
	
	// Update is called once per frame
	void Update () {
        if(gameStarted && currentPlayer == aiPlayer)
        {
            Move m;
            m = minimax.minimax(board, new Move(), 0, 10, aiPlayer);
            bool success = board.ApplyMove(m, aiPlayer);
            if(success)
            {
                boardUI.UpdateBoard(m, board, currentPlayer);
                CheckWinner();
                SwapCurrentPlayer();     
            }
        }
	}

    public void OnClicked(GameObject sender)
    {
        if(gameStarted && currentPlayer == humanPlayer)
        {
            Move m = ConvertToMove(sender.name);
            bool success = board.ApplyMove(m, humanPlayer);
            if(success)
            {
                boardUI.UpdateBoard(m, board, currentPlayer);
                CheckWinner();
                SwapCurrentPlayer();
            }

        }
    }

    public void SetHumanPlayer(int humanPlayer)
    {
        this.humanPlayer = humanPlayer;
        if (humanPlayer == Board.PLAYER_X)
        {
            aiPlayer = Board.PLAYER_O;
            currentPlayer = humanPlayer;
        }
        else
        {
            aiPlayer = Board.PLAYER_X;
            currentPlayer = aiPlayer;
        }

        minimax.SetMaximizingPlayer(aiPlayer);
        minimax.SetMinimizingPlayer(humanPlayer);

        GameObject.Find("IStartButton").GetComponent<Button>().interactable = false;
        GameObject.Find("YouStartButton").GetComponent<Button>().interactable = false;
        boardUI.Clear();
        gameStarted = true;
        gameStatusText.text = "";
    }

    private Move ConvertToMove(string name)
    {
        Move m = new Move();

        switch(name)
        {
            case "BoardPos00":
                m = new Move(0, 0); break;
            case "BoardPos01":
                m = new Move(0, 1); break;
            case "BoardPos02":
                m = new Move(0, 2); break;
            case "BoardPos10":
                m = new Move(1, 0); break;
            case "BoardPos11":
                m = new Move(1, 1); break;
            case "BoardPos12":
                m = new Move(1, 2); break;
            case "BoardPos20":
                m = new Move(2, 0); break;
            case "BoardPos21":
                m = new Move(2, 1); break;
            case "BoardPos22":
                m = new Move(2, 2); break;
        }

        return m;
    }

    private void CheckWinner()
    {
        int winner = board.Winner();
        if (winner == humanPlayer)
        {
            gameStatusText.text = "You win!!!";
            ResetGame();
        }
        else if(winner == aiPlayer)
        {
            gameStatusText.text = "Computer wins!!!";
            ResetGame();
        }
        else if(winner == 2)
        {
            gameStatusText.text = "It's a draw!!!";
            ResetGame();
        }
    }

    private void ResetGame()
    {
        gameStarted = false;
        board = new Board();
        GameObject.Find("IStartButton").GetComponent<Button>().interactable = true;
        GameObject.Find("YouStartButton").GetComponent<Button>().interactable = true;

    }
}
