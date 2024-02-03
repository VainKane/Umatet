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
    public GameObject messange;
    public Text textReward;

    public Text player1Infomation;
    public Text player2Infomation;
    private int player1CoinsInHandCounter;
    private int player2CoinsInHandCounter;

    private int coinsCounter;

    string player1Name;
    string player2Name;

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
         
        messange.SetActive(false);

        player1Name = "Vain_Kane";
        player2Name = "Charles_Kaya";
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
        player1Infomation.text = player1Name + "\n" + "Handing " + player1CoinsInHandCounter;
        player2Infomation.text = player2Name + "\n" + "Handing " + player2CoinsInHandCounter;

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

    IEnumerator CoinsEarner(string moveChoice, int earnedContainerSequence)
    {
        List<GameObject> earnedCoinsList = GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").GetComponent<Counter>().coins;
        if (moveChoice == "go up")
        {
            if (earnedCoinsList.Count != 0)
            {
                for (int i = 0; i < earnedCoinsList.Count; i++)
                {
                    Destroy(earnedCoinsList[i]);
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinSpawner();
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter += 1;
                }
                earnedCoinsList.Clear();

                if (GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").name == "Container (6)")
                {
                    if (isLuckyEnvelope1Hiden == false)
                    {
                        yield return new WaitForSeconds(0.85f);

                        luckyEnvelope1.SetActive(false);
                        messange.SetActive(true);
                        int value;
                        for (int i = 0; i <= 50; i++)
                        {
                            value = UnityEngine.Random.Range(5, 12);
                            textReward.text = "= " + value;
                            yield return new WaitForSeconds(0.95f);

                        }
                        value = UnityEngine.Random.Range(5, 12);
                        textReward.text = "= " + value;
                        yield return new WaitForSeconds(1.5f);
                        for (int i = 0; i < value - 3; i++)
                        {
                            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinSpawner();
                        }
                        messange.SetActive(false);
                        isLuckyEnvelope1Hiden = true;
                    }
                }
                if (GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").name == "Container (12)")
                {
                    if (isLuckyEnvelope2Hiden == false)
                    {
                        yield return new WaitForSeconds(0.85f);

                        luckyEnvelope2.SetActive(false);
                        messange.SetActive(true);
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
                            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinSpawner();
                            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter += 1;
                        }
                        messange.SetActive(false);
                        isLuckyEnvelope2Hiden = true;
                    }
                }

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
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter += 1;

                }
                earnedCoinsList.Clear();

                if (GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").name == "Container (6)")
                {
                    if (isLuckyEnvelope1Hiden == false)
                    {
                        yield return new WaitForSeconds(0.85f);

                        luckyEnvelope1.SetActive(false);
                        messange.SetActive(true);
                        int value;

                        for (int i = 0; i <= 30; i++)
                        {
                            value = UnityEngine.Random.Range(5, 12);
                            textReward.text = "= " + value;
                            yield return new WaitForSeconds(0.1f);

                        }
                        value = UnityEngine.Random.Range(5, 12);
                        textReward.text = "= " + value;
                        yield return new WaitForSeconds(3.5f);

                        for (int i = 0; i < value; i++)
                        {
                            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinSpawner();
                            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter += 1;

                        }
                        messange.SetActive(false);
                        isLuckyEnvelope1Hiden = true;
                    }
                }
                if (GameObject.FindGameObjectWithTag("Container (" + earnedContainerSequence + ")").name == "Container (12)")
                {
                    if (isLuckyEnvelope2Hiden == false)
                    {
                        yield return new WaitForSeconds(0.85f);

                        luckyEnvelope2.SetActive(false);
                        messange.SetActive(true);
                        int value;
                        for (int i = 0; i <= 30; i++)
                        {
                            value = UnityEngine.Random.Range(5, 12);
                            textReward.text = "= " + value;
                            yield return new WaitForSeconds(0.1f);

                        }
                        value = UnityEngine.Random.Range(5, 12);
                        textReward.text = "= " + value;
                        yield return new WaitForSeconds(3.5f);
                        for (int i = 0; i < value - 3; i++)
                        {
                            GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Spawner>().CoinSpawner();
                        }
                        messange.SetActive(false);
                        isLuckyEnvelope2Hiden = true;
                    }
                }

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
        if (GameObject.FindGameObjectWithTag("Container (6)").GetComponent<Counter>().coins.Count == 0 && GameObject.FindGameObjectWithTag("Container (12)").GetComponent<Counter>().coins.Count == 0)
        {
            Debug.Log("gameover!");
        }
        player2CoinsInHandCounter = 0;
        player1CoinsInHandCounter = 0;
        if (playerTurn == "player 1")
        {
            this.playerTurn = "player 2";

            int amountOfEmptyContainers = 0;
            for (int i = 7; i <= 11; i++)
            {
                if (GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Counter>().coins.Count == 0)
                {
                    amountOfEmptyContainers += 1;
                }
            }

            if (amountOfEmptyContainers == 5)
            {
                for (int i = 7; i <= 11; i++)
                {
                    GameObject.FindGameObjectWithTag("Container (" + i + ")").GetComponent<Spawner>().CoinSpawner();
                    Destroy(GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coins[i]);
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter -= 1;
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
                    Destroy(GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coins[i]);
                    GameObject.FindGameObjectWithTag(earnedCoins).GetComponent<Counter>().coinsCounter -= 1;
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
                                GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
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

                        GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
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
                                GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
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

                        GameObject.FindGameObjectWithTag("Container (" + containerSequence + ")").GetComponent<Spawner>().CoinSpawner();
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

                StartCoroutine(CoinsEarner("go up", nextContainerSequence + 1));
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
                StartCoroutine(CoinsEarner("go down", nextContainerSequence - 1));
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