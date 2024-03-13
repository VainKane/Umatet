using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DisplayOnlyUIController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject messangePanel;
    public Text textReward;

    public Text player1ScoreText;
    public Text player2ScoreText;

    [SerializeField] private GameObject gameController;

    [Header("OtherFunctions")]
    [SerializeField] GameObject RedFlag;
    [SerializeField] private GameObject blueFlag;
    [SerializeField] private Text player1Infomation;
    [SerializeField] private Text player2Infomation;
    [SerializeField] private Text result;
    [SerializeField] private GameObject winner;
    public GameObject selection;

    internal string player1Name;
    internal string player2Name;

    private Animator winnerAnimation;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");

        gameOverPanel.SetActive(false);

        RedFlag.SetActive(false);
        blueFlag.SetActive(true);
        selection.SetActive(false);

        messangePanel.SetActive(false);

        winnerAnimation = winner.GetComponent<Animator>();

        player1Name = PlayerPrefs.GetString("player1Name");
        player2Name = PlayerPrefs.GetString("player2Name");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInfomationUpdater();
    }

    public void WhenGameOver(int player1Loan, int player2Loan)
    {
        gameOverPanel.SetActive(true);
        int player1Score = GameObject.Find("Container (c)").GetComponent<Counter>().coinsCounter - player1Loan;
        int player2Score = GameObject.Find("Container (b)").GetComponent<Counter>().coinsCounter - player2Loan;

        player1ScoreText.text = player1Name + "'s" + " Score: " + player1Score;
        player2ScoreText.text = player2Name + "'s" + " Score: " + player2Score;

        result.text = "The winner is ";
        if (player1Score == player2Score)
        {
            result.text = "The game ended in a draw!";
        }
        else if (player1Score > player2Score)
        {
            result.text = String.Concat(result.text, player1Name, "!");
            winnerAnimation.SetFloat("playerHeadSequence", 1);
        }
        else
        {
            result.text = String.Concat(result.text, player2Name, "!");
            winnerAnimation.SetFloat("playerHeadSequence", 2);

        }
    }

    public void PlayerTurnUpdater(string playerTurn)
    {
        if (playerTurn == "player 2")
        {
            RedFlag.SetActive(true);
            blueFlag.SetActive(false);
        }
        if (playerTurn == "player 1")
        {
            RedFlag.SetActive(false);
            blueFlag.SetActive(true);
        }
    }

    public void PlayerInfomationUpdater()
    {
        player1Infomation.text = player1Name + "\nHand: " + gameController.GetComponent<GameMechanic>().player1CoinsInHandCounter + "\nLoan: " + gameController.GetComponent<GameMechanic>().player1Loan;
        player2Infomation.text = player2Name + "\nHand: " + gameController.GetComponent<GameMechanic>().player2CoinsInHandCounter + "\nLoan: " + gameController.GetComponent<GameMechanic>().player2Loan;
    }

    public void SelectingContainer(GameObject container)
    {
        selection.SetActive(true);
        selection.transform.position = container.transform.position;
    }
}
