using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayOnlyUIController : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject gameOverPanel;
    public GameObject selection;
    public GameObject messangePanel;
    public Text textReward;

    [HideInInspector] public Text player1Score;
    [HideInInspector] public Text player2Score;

    [SerializeField] private GameObject gameController;
    [SerializeField] GameObject redFlag;
    [SerializeField] private GameObject blueFlag;
    [SerializeField] private Text player1Infomation;
    [SerializeField] private Text player2Infomation;



    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");

        settingPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        redFlag.SetActive(false);
        blueFlag.SetActive(true);
        selection.SetActive(false);

        messangePanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInfomationUpdater();
    }

    void UsingSettingMenu()
    {
        settingPanel.SetActive(true);
    }

    public void WhenGameOver(int player1Loan, int player2Loan)
    {
        gameOverPanel.SetActive(true);
        player1Score.text = "ads" + "'s" + " Score: " + (GameObject.Find("Container (c)").GetComponent<Counter>().coinsCounter - player1Loan);
        player2Score.text = "asdfasd" + "'s" + " Score: " + (GameObject.Find("Container (b)").GetComponent<Counter>().coinsCounter - player2Loan);
    }

    public void PlayerTurnUpdater(string playerTurn)
    {
        if (playerTurn == "player 2")
        {
            redFlag.SetActive(true);
            blueFlag.SetActive(false);
        }
        if (playerTurn == "player 1")
        {
            redFlag.SetActive(false);
            blueFlag.SetActive(true);
        }
    }

    public void PlayerInfomationUpdater()
    {
        player1Infomation.text = "asdf" + "\nHand: " + gameController.GetComponent<GameMechanic>().player1CoinsInHandCounter + "\nLoan: " + gameController.GetComponent<GameMechanic>().player1Loan;
        player2Infomation.text = "asfa" + "\nHand: " + gameController.GetComponent<GameMechanic>().player2CoinsInHandCounter + "\nLoan: " + gameController.GetComponent<GameMechanic>().player2Loan;
    }

    public void SelectingContainer(GameObject container)
    {
        selection.SetActive(true);
        selection.transform.position = container.transform.position;
    }
}
