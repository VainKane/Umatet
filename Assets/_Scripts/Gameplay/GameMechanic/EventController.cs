using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventController : MonoBehaviour
{
    private List<GameObject> coins;

    public string playerTurn;
    private GameObject caller;
    private string hand;
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

    private int player1Loan;
    private int player2Loan;

    // Start is called before the first frame update
    void Start()
    {
        playerTurn = "player 1";
        delayTime = 0.4f;
        isPlaying = false;
        isAcceptedToPlay = false;

        player1Loan = 0;
        player2Loan = 0;

        isLuckyEnvelope1Hiden = false;
        isLuckyEnvelope2Hiden = false;

        Debug.Log("bruh");
    }

    // Update is called once per frame
    void Update()
    {
        TurnController();
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
            GameObject.FindGameObjectWithTag("Container (d)").GetComponent<Spawner>().CoinSpawner();
        }
        coins.Clear();
        yield return new WaitForSeconds(0.1f);
        isAcceptedToPlay = true;
    }
    IEnumerator WhenPlayer2Turn(GameObject caller)
    {
        coins = GameObject.FindGameObjectWithTag(caller.name).GetComponent<Counter>().coins;
        this.caller = caller;

        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);
            GameObject.FindGameObjectWithTag("Container (a)").GetComponent<Spawner>().CoinSpawner();

        }
        coins.Clear();
        yield return new WaitForSeconds(0.1f);
        isAcceptedToPlay = true;
    }
    public void TurnController()
    {
        if (playerTurn == "player 1")
        {
            hand = "Container (d)";
            earnedCoins = "Container (c)";
            keyIncreasingContainerSequence = KeyCode.LeftArrow;
            keyDecreasingContainerSequence = KeyCode.RightArrow;
        }

        if (playerTurn == "player 2")
        {
            hand = "Container (a)";
            earnedCoins = "Container (b)";
            keyIncreasingContainerSequence = KeyCode.RightArrow;
            keyDecreasingContainerSequence = KeyCode.LeftArrow;
        }
    }
    void OtherFunctionsController()
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

    void playerInteractionDetector()
    {
        if (isAcceptedToPlay == true)
        {
            if (Input.GetKeyDown(keyIncreasingContainerSequence))
            {
                isAcceptedToPlay = false;
                StartCoroutine(UsingTurn("go up", ContainerSequenceCalculator(caller.name)));

            }
            if (Input.GetKey(keyDecreasingContainerSequence))
            {
                isAcceptedToPlay = false;
                StartCoroutine(UsingTurn("go down", ContainerSequenceCalculator(caller.name)));

            }
        }
    }

    void CoinsEarner(string moveChoice, int earnedContainerSequence)
    {
        if (GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").name == "Container (6)")
        {
            if (isLuckyEnvelope1Hiden == false)
            {
                GameObject.FindGameObjectWithTag("LuckyEnvelope (1)").SetActive(false);
                isLuckyEnvelope1Hiden = true;
            }
        }
        if (GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").name == "Container (12)")
        {
            if (isLuckyEnvelope2Hiden == false)
            {
                GameObject.FindGameObjectWithTag("LuckyEnvelope (2)").SetActive(false);
                isLuckyEnvelope2Hiden = true;
            }
        }

        List<GameObject> earnedCoinsList = GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").GetComponent<Counter>().coins;
        if (moveChoice == "go up")
        {
            if (earnedCoinsList.Count != 0)
            {
                for (int i = 0; i < earnedCoinsList.Count; i++)
                {
                    Destroy(earnedCoinsList[i]);
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinSpawner();
                }
                earnedCoinsList.Clear();
                

                if (earnedContainerSequence + 1 == 13)
                {
                    earnedContainerSequence = 0;
                }

                if (GameObject.FindGameObjectWithTag("Container (" + (earnedContainerSequence + 1) + ")").GetComponent<Counter>().coins.Count == 0)
                {
                    StartCoroutine(NextContainerChecker(earnedContainerSequence + 1, moveChoice));
                }
                else
                {
                    PlayerTurnChanger(playerTurn);
                }
            }
            else 
            { 
                PlayerTurnChanger(playerTurn);
            }

        }
        if (moveChoice == "go down")
        {
            if (earnedCoinsList.Count != 0)
            {
                for (int i = 0; i < earnedCoinsList.Count; i++)
                {
                    Destroy(earnedCoinsList[i]);
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinSpawner();
                }
                earnedCoinsList.Clear();

                if (earnedContainerSequence - 1 == 0)
                {
                    earnedContainerSequence = 13;
                }

                if (GameObject.FindGameObjectWithTag("Container (" + (earnedContainerSequence - 1) + ")").GetComponent<Counter>().coins.Count == 0)
                {
                    StartCoroutine(NextContainerChecker(earnedContainerSequence - 1, moveChoice));
                }
                else
                {
                    PlayerTurnChanger(playerTurn);
                }
            }
            else
            {
                PlayerTurnChanger(playerTurn);
            }
        }

    }

    void PlayerTurnChanger(string playerTurn)
    {
        if (playerTurn == "player 1")
        {
            this.playerTurn = "player 2";

            int amountOfEmptyContainers = 0;
            for (int i = 7; i <= 11; i++)
            {
                if (GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Count == 0)
                    amountOfEmptyContainers += 1;
            }
            if (amountOfEmptyContainers == 5)
            {
                for (int i = 7; i <= 11; i++)
                {
                    GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Spawner>().CoinSpawner();
                    player2Loan += 1;
                }
            }
        }
        else
        {
            this.playerTurn = "player 1";

            int amountOfEmptyContainers = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Count == 0)
                    amountOfEmptyContainers += 1;
            }
            if (amountOfEmptyContainers == 5)
            {
                for (int i = 1; i <= 5; i++)
                {
                    GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Spawner>().CoinSpawner();
                    player1Loan += 1;
                }
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

    IEnumerator UsingTurn(string moveChoice, int containerSequence)
    {
        if (moveChoice == "go up")
        {
            if (isPlaying == false)
            {
                isPlaying = true;
                List<GameObject> handedCoins = GameObject.FindGameObjectWithTag(hand).GetComponent<Counter>().coins;

                for (int i = 1; i < handedCoins.Count + 1; i++)
                {
                    containerSequence += 1;

                    if (containerSequence > 12)
                    {
                    isContainerSequenceHigherThan12:
                        for (int n = 0; n <= handedCoins.Count - i; n++)
                        {
                            containerSequence = 1;
                            containerSequence += n;
                            if (containerSequence > 12)
                            {
                                i += 12;
                                goto isContainerSequenceHigherThan12;
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
                                yield return new WaitForSeconds(delayTime);

                            }
                        }
                        i = 9999;

                    }
                    else
                    {

                        GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
                        yield return new WaitForSeconds(delayTime);

                    }
                }

                for (int i = 0; i < handedCoins.Count; i++)
                {
                    Destroy(handedCoins[i]);
                }
                handedCoins.Clear();
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
                List<GameObject> handedCoins = GameObject.FindGameObjectWithTag(hand).GetComponent<Counter>().coins;

                for (int i = 1; i < handedCoins.Count + 1; i++)
                {
                    containerSequence -= 1;

                    if (containerSequence < 1)
                    {
                    isContainerSequenceLowerThan1:
                        for (int n = 0; n <= handedCoins.Count - i; n++)
                        {
                            containerSequence = 12;
                            containerSequence -= n;
                            if (containerSequence < 1)
                            {
                                i += 12;
                                goto isContainerSequenceLowerThan1;
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
                                yield return new WaitForSeconds(delayTime);

                            }
                        }
                        i = 9999;

                    }
                    else
                    {

                        GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
                        yield return new WaitForSeconds(delayTime);

                    }
                }

                for (int i = 0; i < handedCoins.Count; i++)
                {
                    Destroy(handedCoins[i]);
                }
                handedCoins.Clear();
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

                CoinsEarner("go up", nextContainerSequence + 1);
                yield return new WaitForSeconds(1);

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
                        GameObject.FindGameObjectWithTag(hand).GetComponent<Spawner>().CoinSpawner();
                        Destroy(GameObject.FindGameObjectWithTag("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins[i]);
                    }
                    coins.Clear();
                    yield return new WaitForSeconds(1);
                    isPlaying = false;
                    StartCoroutine(UsingTurn(moveChoice, nextContainerSequence));
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
                CoinsEarner("go down", nextContainerSequence - 1);
                yield return new WaitForSeconds(1);
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
                        GameObject.FindGameObjectWithTag(hand).GetComponent<Spawner>().CoinSpawner();
                        Destroy(GameObject.FindGameObjectWithTag("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins[i]);
                    }
                    coins.Clear();
                    yield return new WaitForSeconds(1);
                    isPlaying = false;
                    StartCoroutine(UsingTurn(moveChoice, nextContainerSequence));
                }
            }
        }
    }
}