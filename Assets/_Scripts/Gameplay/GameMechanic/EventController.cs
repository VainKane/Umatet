using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class EventController : MonoBehaviour
{
    private List<GameObject> coins;

    public string playerTurn;
    private GameObject caller;
    private string earnedCoins;
    private float delayTime;

    private KeyCode keyIncreasingContainerSequence;
    private KeyCode keyDecreasingContainerSequence;

    public GameObject redFlag;
    public GameObject blueFlag;

    private bool isAcceptedToPlay;
    public bool isPlaying;

    private bool isLuckyEnvelope1Hiden;
    private bool isLuckyEnvelope2Hiden;

    public GameObject luckyEnvelope1;
    public GameObject luckyEnvelope2;
    public GameObject messangePanel;
    public Text textReward;

    public Text player1Infomation;
    public Text player2Infomation;
    private int player1CoinsInHandCounter;
    private int player2CoinsInHandCounter;

    private int coinsCounter;

    string player1Name;
    string player2Name;

    public bool isAcceptedToClick;

    public GameObject gameOverPanel;
    public Text player1InfomationText;
    public Text player2InfomationText;

    private bool isGameOver;

    //public GameObject selection;

    int player1Loan;
    int player2Loan;

    // Start is called before the first frame update
    void Start()
    {
        playerTurn = "player 1";
        delayTime = 0.37f;
        isPlaying = false;
        isAcceptedToPlay = false;

        player1CoinsInHandCounter = 0;
        player2CoinsInHandCounter = 0;

        isLuckyEnvelope1Hiden = false;
        isLuckyEnvelope2Hiden = false;

        messangePanel.SetActive(false);

        player1Name = "Vain_Kane";
        player2Name = "Charles_Kaya";

        isAcceptedToClick = true;

        gameOverPanel.SetActive(false);
        isGameOver = false;

        //selection.SetActive(false);

        player1Loan = 0;
        player2Loan = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InfomationUpdater();
        playerInteractionDetector();
        OtherFunctionsController();



    }

    public void Player1ClickReceiver(GameObject caller)
    {
        StartCoroutine(WhenPlayer1Turn(caller));
    }

    public void Player2ClickReceiver(GameObject caller)
    {
        StartCoroutine(WhenPlayer2Turn(caller));
    }

    IEnumerator WhenPlayer1Turn(GameObject caller)
    {
        coins = caller.GetComponent<Counter>().coins;
        this.caller = caller;

        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);
        }
        coinsCounter = coins.Count;
        player1CoinsInHandCounter = coins.Count;
        coins.Clear();
        yield return new WaitForSeconds(0.1f);
        isAcceptedToPlay = true;
        //selection.SetActive(true);
        //selection.transform.position = caller.transform.position;


    }

    IEnumerator WhenPlayer2Turn(GameObject caller)
    {
        coins = GameObject.FindGameObjectWithTag(caller.name).GetComponent<Counter>().coins;
        this.caller = caller;

        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);

        }
        coinsCounter = coins.Count;
        player2CoinsInHandCounter = coins.Count;

        coins.Clear();
        yield return new WaitForSeconds(0.1f);
        isAcceptedToPlay = true;

        //selection.SetActive(true);
        //selection.transform.position = caller.transform.position;
    }

    public void InfomationUpdater()
    {
        if (playerTurn == "player 1")
        {
            earnedCoins = "Container (c)";
            keyIncreasingContainerSequence = KeyCode.LeftArrow;
            keyDecreasingContainerSequence = KeyCode.RightArrow;
        }

        if (playerTurn == "player 2")
        {
            earnedCoins = "Container (b)";
            keyIncreasingContainerSequence = KeyCode.RightArrow;
            keyDecreasingContainerSequence = KeyCode.LeftArrow;
        }
    }

    void OtherFunctionsController()
    {
        player1Infomation.text = player1Name + "\nHanding " + player1CoinsInHandCounter + "\nLoan: " + player1Loan;
        player2Infomation.text = player2Name + "\nHanding " + player2CoinsInHandCounter + "\nLoan: " + player2Loan;

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

    void playerInteractionDetector()
    {
        if (isAcceptedToPlay == true)
        {
            if (Input.GetKeyDown(keyIncreasingContainerSequence))
            {
                isAcceptedToPlay = false;
                StartCoroutine(UsingTurn("go up", ContainerSequenceCalculator(caller.name), coinsCounter));

            }
            if (Input.GetKey(keyDecreasingContainerSequence))
            {
                isAcceptedToPlay = false;
                StartCoroutine(UsingTurn("go down", ContainerSequenceCalculator(caller.name), coinsCounter));
            }
        }
    }

    IEnumerator LuckyEnvelopeEarner(string containerName)
    {

        if (containerName == "Container (6)")
        {
            isLuckyEnvelope1Hiden = true;
            luckyEnvelope1.SetActive(false);

        }
        else
        {
            isLuckyEnvelope2Hiden = true;
            luckyEnvelope2.SetActive(false);
        }


        yield return new WaitForSeconds(0.85f);

        messangePanel.SetActive(true);
        int value;

        for (int i = 0; i <= 50; i++)
        {
            value = UnityEngine.Random.Range(5, 12);
            textReward.text = "= " + value;
            yield return new WaitForSeconds(0.1f);

        }

        value = UnityEngine.Random.Range(5, 12);
        textReward.text = "= " + value;
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < value; i++)
        {
            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinsEarner();
        }

        messangePanel.SetActive(false);
    }

    void CoinsEarner(List<GameObject> earnedCoinsList)
    {
        for (int i = 0; i < earnedCoinsList.Count; i++)
        {
            Destroy(earnedCoinsList[i]);
            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinsEarner();
        }
        earnedCoinsList.Clear();
    }

    IEnumerator EarnedContainerChecker(string moveChoice, int earnedContainerSequence)
    {
        List<GameObject> earnedCoinsList = GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").GetComponent<Counter>().coins;

        if (earnedCoinsList.Count != 0)
        {
            if (earnedContainerSequence == 6)
            {
                if (isLuckyEnvelope1Hiden == false)
                {
                    if (GameObject.FindGameObjectWithTag("Container (6)").GetComponent<Counter>().coins.Count >= 5)
                    {
                        StartCoroutine(LuckyEnvelopeEarner("Container (6)"));
                        CoinsEarner(earnedCoinsList);
                        yield return new WaitForSeconds(8.85f);
                    }
                }
                else
                {
                    CoinsEarner(earnedCoinsList);
                }
            }
            else if (earnedContainerSequence == 12)
            {
                if (isLuckyEnvelope2Hiden == false)
                {
                    if (isLuckyEnvelope2Hiden == false)
                    {
                        if (GameObject.FindGameObjectWithTag("Container (12)").GetComponent<Counter>().coins.Count >= 5)
                        {
                            StartCoroutine(LuckyEnvelopeEarner("Container (12)"));
                            CoinsEarner(earnedCoinsList);
                            yield return new WaitForSeconds(8.85f);

                        }
                    }
                }
                else
                {
                    CoinsEarner(earnedCoinsList);

                }
            }
            else
            {
                CoinsEarner(earnedCoinsList);

            }

            if (moveChoice == "go up")
            {
                if (earnedContainerSequence + 1 == 13)
                {
                    earnedContainerSequence = 0;
                }

                if (GameObject.FindGameObjectWithTag("Container (" + (earnedContainerSequence + 1) + ")").GetComponent<Counter>().coins.Count == 0)
                {
                    yield return new WaitForSeconds(0.85f);
                    StartCoroutine(NextContainerChecker(earnedContainerSequence + 1, moveChoice));
                }
                else
                {
                    PlayerTurnChanger(playerTurn);
                }
            }
            if (moveChoice == "go down")
            {
                if (earnedContainerSequence - 1 == 0)
                {
                    earnedContainerSequence = 13;
                }

                if (GameObject.FindGameObjectWithTag("Container (" + (earnedContainerSequence - 1) + ")").GetComponent<Counter>().coins.Count == 0)
                {
                    yield return new WaitForSeconds(0.85f);
                    StartCoroutine(NextContainerChecker(earnedContainerSequence - 1, moveChoice));
                }
                else
                {
                    PlayerTurnChanger(playerTurn);
                }
            }
        }
        else
        {
            PlayerTurnChanger(playerTurn);
        }
    }

    IEnumerator GameOverChecker()
    {
        if (GameObject.FindGameObjectWithTag("Container (6)").GetComponent<Counter>().coins.Count == 0 && GameObject.FindGameObjectWithTag("Container (12)").GetComponent<Counter>().coins.Count == 0)
        {
            isAcceptedToClick = false;
            isGameOver = true;

            yield return new WaitForSeconds(1.5f);
            for (int i = 1; i <= 5; i++)
            {
                for (int n = 0; n < GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Count; n++)
                {
                    Destroy(GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins[n]);
                    GameObject.FindGameObjectWithTag("Container (c)").GetComponent<Spawner>().CoinsEarner();
                }
                GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Clear();

            }
            for (int i = 7; i <= 11; i++)
            {
                for (int n = 0; n < GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Count; n++)
                {
                    Destroy(GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins[n]);
                    GameObject.FindGameObjectWithTag("Container (b)").GetComponent<Spawner>().CoinsEarner();

                }
                GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Clear();

            }

            yield return new WaitForSeconds(1.5f);
            gameOverPanel.SetActive(true);
            player1InfomationText.text = player1Name + "'s" + " Score: " + GameObject.FindGameObjectWithTag("Container (c)").GetComponent<Counter>().coinsCounter;
            player2InfomationText.text = player2Name + "'s" + " Score: " + GameObject.FindGameObjectWithTag("Container (b)").GetComponent<Counter>().coinsCounter;
        }
    }

    IEnumerator EmptyContainersChecker(string playerTurn)
    {
        isPlaying = true;
        isAcceptedToClick = false;

        string earnedCoins;
        int firstContainerSequence;
        int lastContainerSequence;

        if (playerTurn == "player 2")
        {
            earnedCoins = "Container (b)";
            firstContainerSequence = 7;
            lastContainerSequence = 11;

        }
        else
        {
            earnedCoins = "Container (c)";
            firstContainerSequence = 1;
            lastContainerSequence = 5;
        }

        int amountOfEmptyContainers = 0;
        for (int i = firstContainerSequence; i <= lastContainerSequence; i++)
        {
            if (GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Count == 0)
            {
                amountOfEmptyContainers += 1;
            }
        }


        if (amountOfEmptyContainers == 5)
        {
            if (GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter < 5)
            {
                if (playerTurn == "player1")
                {
                    player1Loan += 5 - GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter;
                }
                else
                {
                    player2Loan += 5 - GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter;
                }

                for (int i = 0; i < 5 - GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter; i++)
                {
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinsSpawner();
                }
            }

            if (GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter >= 5)
            {
                int coinConsequence = -1;
                for (int i = firstContainerSequence; i <= lastContainerSequence; i++)
                {
                    yield return new WaitForSeconds(0.25f);
                    coinConsequence += 1;
                    GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Spawner>().CoinsSpawner();

                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinsDestroyer(coinConsequence);
                }
            }
        }


        isPlaying = false;
        isAcceptedToClick = true;
    }

    void PlayerTurnChanger(string playerTurn)
    {
        StartCoroutine(GameOverChecker());

        player2CoinsInHandCounter = 0;
        player1CoinsInHandCounter = 0;

        if (playerTurn == "player 1")
        {
            this.playerTurn = "player 2";
            if (isGameOver == false)
            {
                StartCoroutine(EmptyContainersChecker(this.playerTurn));
            }
        }
        else
        {
            this.playerTurn = "player 1";
            if (isGameOver == false)
            {
                StartCoroutine(EmptyContainersChecker(this.playerTurn));
            }
        }

        isPlaying = false;
    }

    int ContainerSequenceCalculator(string containerName)
    {
        containerName = containerName.Substring(11);
        int containerSequence = Convert.ToInt16(containerName.Remove(containerName.LastIndexOf(")")));
        return containerSequence;
    }

    IEnumerator UsingTurn(string moveChoice, int containerSequence, int coinsCounter)
    {

        if (moveChoice == "go up")
        {
            if (isPlaying == false)
            {
                isPlaying = true;

                for (int i = 1; i < coinsCounter + 1; i++)
                {
                    containerSequence += 1;
                    if (containerSequence > 12)
                    {
                    isContainerSequenceHigherThan12:
                        for (int n = 0; n <= coinsCounter - i; n++)
                        {
                            containerSequence = 1;
                            containerSequence += n;
                            if (playerTurn == "player 1")
                            {
                                player1CoinsInHandCounter -= 1;
                            }
                            else
                            {
                                player2CoinsInHandCounter -= 1;
                            }
                            if (containerSequence > 12)
                            {
                                i += 12;
                                goto isContainerSequenceHigherThan12;
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
                                yield return new WaitForSeconds(delayTime);

                            }
                        }
                        i = 9999;

                    }


                    else
                    {
                        if (playerTurn == "player 1")
                        {
                            player1CoinsInHandCounter -= 1;
                        }
                        else
                        {
                            player2CoinsInHandCounter -= 1;
                        }

                        GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
                        yield return new WaitForSeconds(delayTime);

                    }

                }

                isPlaying = false;
            }
            if (containerSequence + 1 == 13)
            {
                containerSequence = 0;
            }
            StartCoroutine(NextContainerChecker(containerSequence + 1, moveChoice));
        }



        if (moveChoice == "go down")
        {
            if (isPlaying == false)
            {
                isPlaying = true;

                for (int i = 1; i < coinsCounter + 1; i++)
                {
                    containerSequence -= 1;

                    if (containerSequence < 1)
                    {
                    isContainerSequenceLowerThan1:
                        for (int n = 0; n <= coinsCounter - i; n++)
                        {
                            containerSequence = 12 - n;
                            if (playerTurn == "player 1")
                            {
                                player1CoinsInHandCounter -= 1;
                            }
                            else
                            {
                                player2CoinsInHandCounter -= 1;
                            }
                            if (containerSequence < 1)
                            {
                                i += 12;
                                goto isContainerSequenceLowerThan1;
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
                                yield return new WaitForSeconds(delayTime);

                            }
                        }
                        i = 9999;

                    }
                    else
                    {
                        if (playerTurn == "player 1")
                        {
                            player1CoinsInHandCounter -= 1;
                        }
                        else
                        {
                            player2CoinsInHandCounter -= 1;
                        }

                        GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
                        yield return new WaitForSeconds(delayTime);

                    }
                }


            }
            if (containerSequence - 1 == 0)
            {
                containerSequence = 13;
            }
            StartCoroutine(NextContainerChecker(containerSequence - 1, moveChoice));
        }

    }

    IEnumerator NextContainerChecker(int nextContainerSequence, string moveChoice)
    {
        List<GameObject> coins = GameObject.FindGameObjectWithTag("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins;
        if (moveChoice == "go up")
        {
            if (coins.Count == 0)
            {
                if (nextContainerSequence == 12)
                {
                    nextContainerSequence = 0;
                }

                StartCoroutine(EarnedContainerChecker("go up", nextContainerSequence + 1));
                yield return new WaitForSeconds(0.85f);

            }
            else
            {
                if (nextContainerSequence == 6 || nextContainerSequence == 12)
                {
                    PlayerTurnChanger(playerTurn);
                }
                else
                {
                    for (int i = 0; i < coins.Count; i++)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins[i]);
                    }
                    int n = coins.Count;
                    if (playerTurn == "player 1")
                    {
                        player1CoinsInHandCounter = n;
                    }
                    else
                    {
                        player2CoinsInHandCounter = n;
                    }
                    coins.Clear();
                    yield return new WaitForSeconds(0.85f);
                    isPlaying = false;

                    StartCoroutine(UsingTurn(moveChoice, nextContainerSequence, n));
                }
            }
        }
        if (moveChoice == "go down")
        {
            if (coins.Count == 0)
            {
                if (nextContainerSequence == 1)
                {
                    nextContainerSequence = 13;
                }
                StartCoroutine(EarnedContainerChecker("go down", nextContainerSequence - 1));
                yield return new WaitForSeconds(0.85f);
            }
            else
            {
                if (nextContainerSequence == 6 || nextContainerSequence == 12)
                {
                    PlayerTurnChanger(playerTurn);
                }
                else
                {
                    for (int i = 0; i < coins.Count; i++)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins[i]);
                    }
                    int n = coins.Count;
                    if (playerTurn == "player 1")
                    {
                        player1CoinsInHandCounter = n;
                    }
                    else
                    {
                        player2CoinsInHandCounter = n;
                    }

                    coins.Clear();
                    yield return new WaitForSeconds(0.85f);
                    isPlaying = false;
                    StartCoroutine(UsingTurn(moveChoice, nextContainerSequence, n));
                }
            }
        }
    }
}