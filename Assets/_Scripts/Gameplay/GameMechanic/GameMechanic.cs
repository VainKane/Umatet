using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameMechanic : MonoBehaviour
{
    [SerializeField] private GameObject uIController;
    [SerializeField] private GameObject luckyEnvelope1;
    [SerializeField] private GameObject luckyEnvelope2;


    [HideInInspector] public KeyCode keyIncreasingContainerSequence;
    [HideInInspector] public KeyCode keyDecreasingContainerSequence;
    [HideInInspector] public int player1CoinsInHandCounter;
    [HideInInspector] public int player2CoinsInHandCounter;
    [HideInInspector] public int coinsCounter;
    [HideInInspector] public int player1Loan;
    [HideInInspector] public int player2Loan;
    [HideInInspector] public bool isAcceptedToPlay;
    [HideInInspector] public GameObject container;
    [HideInInspector] public bool isPlaying;
    [HideInInspector] public string playerTurn;
    [HideInInspector] public bool isAcceptedToClick;

    private List<GameObject> coins;
    private bool isLuckyEnvelope1Hiden;
    private bool isLuckyEnvelope2Hiden;
    private bool isGameOver;
    private string earnedCoins;
    private float delayTime;

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

        isAcceptedToClick = true;
        isGameOver = false;

        player1Loan = 0;
        player2Loan = 0;

        uIController = GameObject.Find("UIController");
    }

    // Update is called once per frame
    void Update()
    {
        AttributesUpdater();
    }

    public void PlayerClickReceiver(GameObject container)
    {
        StartCoroutine(ChosingContainer(container));
    }

    IEnumerator ChosingContainer(GameObject container)
    {
        coins = container.GetComponent<Counter>().coins;
        this.container = container;

        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);
        }

        coinsCounter = coins.Count;
        player1CoinsInHandCounter = coins.Count;
        coins.Clear();
        uIController.GetComponent<DisplayOnlyUIController>().SelectingContainer(container);

        yield return new WaitForSeconds(0.1f);
        isAcceptedToPlay = true;
    }

    public void AttributesUpdater()
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

        uIController.GetComponent<DisplayOnlyUIController>().messangePanel.SetActive(true);
        int value;

        for (int i = 0; i <= 50; i++)
        {
            value = UnityEngine.Random.Range(5, 12);
            uIController.GetComponent<DisplayOnlyUIController>().textReward.text = "= " + value;
            yield return new WaitForSeconds(0.1f);

        }

        value = UnityEngine.Random.Range(5, 12);
        uIController.GetComponent<DisplayOnlyUIController>().textReward.text = "= " + value;
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < value; i++)
        {
            GameObject.Find(earnedCoins).GetComponent<Spawner>().CoinsEarner();
        }

        uIController.GetComponent<DisplayOnlyUIController>().messangePanel.SetActive(false);
    }

    void CoinsEarner(List<GameObject> earnedCoinsList)
    {
        for (int i = 0; i < earnedCoinsList.Count; i++)
        {
            Destroy(earnedCoinsList[i]);
            GameObject.Find(earnedCoins).GetComponent<Spawner>().CoinsEarner();
        }
        earnedCoinsList.Clear();
    }

    IEnumerator EarnedContainerChecker(string moveChoice, int earnedContainerSequence)
    {
        List<GameObject> earnedCoinsList = GameObject.Find("Container (" + earnedContainerSequence + ")").GetComponent<Counter>().coins;

        if (earnedCoinsList.Count != 0)
        {
            if (earnedContainerSequence == 6)
            {
                if (isLuckyEnvelope1Hiden == false)
                {
                    if (GameObject.Find("Container (6)").GetComponent<Counter>().coins.Count >= 5)
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
                        if (GameObject.Find("Container (12)").GetComponent<Counter>().coins.Count >= 5)
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

                if (GameObject.Find("Container (" + (earnedContainerSequence + 1) + ")").GetComponent<Counter>().coins.Count == 0)
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

                if (GameObject.Find("Container (" + (earnedContainerSequence - 1) + ")").GetComponent<Counter>().coins.Count == 0)
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
        if (GameObject.Find("Container (6)").GetComponent<Counter>().coins.Count == 0 && GameObject.Find("Container (12)").GetComponent<Counter>().coins.Count == 0)
        {
            isAcceptedToClick = false;
            isGameOver = true;

            yield return new WaitForSeconds(1.5f);
            for (int i = 1; i <= 5; i++)
            {
                for (int n = 0; n < GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Count; n++)
                {
                    Destroy(GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins[n]);
                    GameObject.Find("Container (c)").GetComponent<Spawner>().CoinsEarner();
                }
                GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Clear();

            }
            for (int i = 7; i <= 11; i++)
            {
                for (int n = 0; n < GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Count; n++)
                {
                    Destroy(GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins[n]);
                    GameObject.Find("Container (b)").GetComponent<Spawner>().CoinsEarner();

                }
                GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Clear();

            }

            yield return new WaitForSeconds(1.5f);

        uIController.GetComponent<DisplayOnlyUIController>().WhenGameOver(player1Loan, player2Loan);
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
            if (GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Count == 0)
            {
                amountOfEmptyContainers += 1;
            }
        }


        if (amountOfEmptyContainers == 5)
        {
            if (GameObject.Find(earnedCoins).GetComponent<Counter>().coinsCounter < 5)
            {
                if (playerTurn == "player1")
                {
                    player1Loan += (5 - GameObject.Find(earnedCoins).GetComponent<Counter>().coinsCounter);
                }
                else
                {
                    player2Loan += (5 - GameObject.Find(earnedCoins).GetComponent<Counter>().coinsCounter);
                }
                int amountOfCoins = GameObject.Find(earnedCoins).GetComponent<Counter>().coinsCounter;

                for (int i = 0; i < (5 - amountOfCoins); i++)
                {
                    GameObject.Find(earnedCoins).GetComponent<Spawner>().CoinsEarner();
                }
                yield return new WaitForSeconds(0.85f);
                
            }
            
            if (GameObject.Find(earnedCoins).GetComponent<Counter>().coinsCounter >= 5)
            {

                int coinSequence = -1;
                for (int i = firstContainerSequence; i <= lastContainerSequence; i++)
                {
                    yield return new WaitForSeconds(0.25f);
                    coinSequence += 1;
                    GameObject.Find("Container (" + i + ")").GetComponent<Spawner>().CoinsSpawner();
                    GameObject.Find(earnedCoins).GetComponent<Spawner>().CoinsDestroyer();
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

        uIController.GetComponent<DisplayOnlyUIController>().PlayerTurnUpdater(this.playerTurn);
        isPlaying = false;
    }

    public int ContainerSequenceCalculator(string containerName)
    {
        containerName = containerName.Substring(11);
        int containerSequence = Convert.ToInt16(containerName.Remove(containerName.LastIndexOf(")")));
        return containerSequence;
    }

    public IEnumerator UsingTurn(string moveChoice, int containerSequence, int coinsCounter)
    {
        uIController.GetComponent<DisplayOnlyUIController>().selection.SetActive(false);

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
                                GameObject.Find("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
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

                        GameObject.Find("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
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
                                GameObject.Find("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
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

                        GameObject.Find("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinsSpawner();
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
        List<GameObject> coins = GameObject.Find("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins;
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
                        Destroy(GameObject.Find("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins[i]);
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
                        Destroy(GameObject.Find("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins[i]);
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