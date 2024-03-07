using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameMechanic : MonoBehaviour
{
    [SerializeField] private GameObject uIController;
    [SerializeField] private GameObject RedEnvelope1;
    [SerializeField] private GameObject RedEnvelope2;


    [HideInInspector] public KeyCode keyIncreasingContainerSequence;
    [HideInInspector] public KeyCode keyDecreasingContainerSequence;

    [HideInInspector] public int player1CoinsInHandCounter;
    [HideInInspector] public int player2CoinsInHandCounter;

    [HideInInspector] public int coinsCounter;

    [HideInInspector] public int player1Loan;
    [HideInInspector] public int player2Loan;

    [HideInInspector] public string playerTurn;
    [HideInInspector] public GameObject container;

    [HideInInspector] public bool isAcceptedToPlay;
    [HideInInspector] public bool isPlaying;
    [HideInInspector] public bool isAcceptedToClick;
    [HideInInspector] static public bool isPlayingSavedGame;

    [HideInInspector] public bool isRedEnvelope1Hiden;
    [HideInInspector] public bool isRedEnvelope2Hiden;

    private List<GameObject> coins;
    private bool isGameOver;
    private string earnedCoins;
    private float delayTime;
    private AudioController audioController;


    // Start is called before the first frame update
    void Start()
    {
        delayTime = 0.37f;
        isPlaying = false;
        isAcceptedToPlay = false;

        player1CoinsInHandCounter = 0;
        player2CoinsInHandCounter = 0;

        isAcceptedToClick = true;
        isGameOver = false;

        uIController = GameObject.Find("UIController");
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

        if (PlayerPrefsExtra.GetBool("isPlayingSavedGame") == false)
        {
            playerTurn = "player 1";

            isRedEnvelope1Hiden = false;
            isRedEnvelope2Hiden = false;

            player1Loan = 0;
            player2Loan = 0;
        }
        else
        {
            playerTurn = PlayerPrefs.GetString("playerTurn");

            isRedEnvelope1Hiden = PlayerPrefsExtra.GetBool("isRedEnvelope1Hiden");
            isRedEnvelope2Hiden = PlayerPrefsExtra.GetBool("isRedEnvelope2Hiden");

            player1Loan = PlayerPrefs.GetInt("player1Loan");
            player2Loan = PlayerPrefs.GetInt("player2Loan");
        }

    }

    // Update is called once per frame
    void Update()
    {
        AttributesUpdater();

    }

    private IEnumerator WhenBotTurn()
    {
        int emptyContainersCounter = 0;
        List<int> checkedContainersSequence = new List<int> { };

    chosingContainer:
        int containerSequence = UnityEngine.Random.Range(7, 12);
        if (checkedContainersSequence.Contains(containerSequence) == false)
        {
            checkedContainersSequence.Add(containerSequence);
            if (GameObject.Find("Container (" + containerSequence + ")").GetComponent<Counter>().coins.Count > 0)
            {
                yield return new WaitForSeconds(delayTime);
                StartCoroutine(ChosingContainer(GameObject.Find("Container (" + containerSequence + ")")));
            }
            else
            {
                emptyContainersCounter += 1;
                goto chosingContainer;
            }
        }
        else
        {
            if (emptyContainersCounter == 5)
            {
                StartCoroutine(EmptyContainersChecker("player 2"));
                yield return new WaitForSeconds(2);
                StartCoroutine(ChosingContainer(GameObject.Find("Container (" + containerSequence + ")")));
            }
            else
            {
                goto chosingContainer;

            }
        }
        yield return new WaitForSeconds(delayTime);

        List<string> moveChoices = new List<string> { "go up", "go down" };
        StartCoroutine(UsingTurn(false, moveChoices[UnityEngine.Random.Range(0, 1)], ContainerSequenceGetter(container.name), player2CoinsInHandCounter));

    }

    public void PlayerClickReceiver(GameObject container)
    {
        StartCoroutine(ChosingContainer(container));
        isAcceptedToPlay = true;
        isAcceptedToClick = false;
        Spawner.isTheFirstTimePlaying = false;
        AttributesUpdater();
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

        if (playerTurn == "player 1")
        {
            player1CoinsInHandCounter = coins.Count;
        }
        else
        {
            player2CoinsInHandCounter = coins.Count;
        }

        coins.Clear();
        uIController.GetComponent<DisplayOnlyUIController>().SelectingContainer(container);
        audioController.PlayingSFX(audioController.pickingCoin);

        yield return new WaitForSeconds(0.1f);
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

    IEnumerator PlayerTurnChanger()
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(GameOverChecker());

        player2CoinsInHandCounter = 0;
        player1CoinsInHandCounter = 0;

        if (isGameOver == false)
        {
            if (playerTurn == "player 1")
            {
                this.playerTurn = "player 2";

                if (PlayerPrefsExtra.GetBool("isOnBot") == true)
                {
                    StartCoroutine(WhenBotTurn());
                }
                else
                {
                    StartCoroutine(EmptyContainersChecker(this.playerTurn));
                }

            }
            else
            {
                this.playerTurn = "player 1";
                StartCoroutine(EmptyContainersChecker(this.playerTurn));

            }


            uIController.GetComponent<DisplayOnlyUIController>().PlayerTurnUpdater(this.playerTurn);
            isPlaying = false;
            isAcceptedToClick = true;
        }

    }

    IEnumerator RedEnvelopeEarner(string containerName)
    {
        if (containerName == "Container (6)")
        {
            isRedEnvelope1Hiden = true;
            RedEnvelope1.SetActive(false);
        }
        else
        {
            isRedEnvelope2Hiden = true;
            RedEnvelope2.SetActive(false);
        }

        uIController.GetComponent<DisplayOnlyUIController>().messangePanel.SetActive(true);
        int value;

        for (int i = 0; i <= 50; i++)
        {
            value = UnityEngine.Random.Range(5, 12);
            uIController.GetComponent<DisplayOnlyUIController>().textReward.text = "= " + value;
            audioController.PlayingSFX(audioController.ping);
            yield return new WaitForSeconds(0.1f);
        }

        value = UnityEngine.Random.Range(5, 12);
        uIController.GetComponent<DisplayOnlyUIController>().textReward.text = "= " + value;
        audioController.PlayingSFX(audioController.ping);
        yield return new WaitForSeconds(1.2f);


        for (int i = 0; i < value; i++)
        {
            GameObject.Find(earnedCoins).GetComponent<Spawner>().CoinsSpawner();
        }
        audioController.PlayingSFX(audioController.earningCoin);

        uIController.GetComponent<DisplayOnlyUIController>().messangePanel.SetActive(false);
    }

    void CoinsEarner(List<GameObject> earnedCoinsList)
    {
        audioController.PlayingSFX(audioController.earningCoin);
        for (int i = 0; i < earnedCoinsList.Count; i++)
        {
            Destroy(earnedCoinsList[i]);
            GameObject.Find(earnedCoins).GetComponent<Spawner>().CoinsSpawner();
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
                if (isRedEnvelope1Hiden == false)
                {
                    if (GameObject.Find("Container (6)").GetComponent<Counter>().coins.Count >= 5)
                    {
                        yield return new WaitForSeconds(delayTime);
                        StartCoroutine(RedEnvelopeEarner("Container (6)"));
                        CoinsEarner(earnedCoinsList);
                        yield return new WaitForSeconds(7.1f);
                    }
                }
                else
                {
                    CoinsEarner(earnedCoinsList);
                }
            }
            else if (earnedContainerSequence == 12)
            {
                if (isRedEnvelope2Hiden == false)
                {
                    if (isRedEnvelope2Hiden == false)
                    {
                        if (GameObject.Find("Container (12)").GetComponent<Counter>().coins.Count >= 5)
                        {
                            yield return new WaitForSeconds(delayTime);
                            StartCoroutine(RedEnvelopeEarner("Container (12)"));
                            CoinsEarner(earnedCoinsList);
                            yield return new WaitForSeconds(7.1f);

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
                    yield return new WaitForSeconds(delayTime);
                    NextContainerChecker(earnedContainerSequence + 1, moveChoice);
                }
                else
                {
                    StartCoroutine(PlayerTurnChanger());
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
                    yield return new WaitForSeconds(delayTime);
                    NextContainerChecker(earnedContainerSequence - 1, moveChoice);
                }
                else
                {
                    StartCoroutine(PlayerTurnChanger());
                }
            }
        }
        else
        {
            StartCoroutine(PlayerTurnChanger());
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
                    GameObject.Find("Container (c)").GetComponent<Spawner>().CoinsSpawner();
                }
                GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Clear();

            }
            for (int i = 7; i <= 11; i++)
            {
                for (int n = 0; n < GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Count; n++)
                {
                    Destroy(GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins[n]);
                    GameObject.Find("Container (b)").GetComponent<Spawner>().CoinsSpawner();

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
        else if (playerTurn == "player 1")
        {
            earnedCoins = "Container (c)";
            firstContainerSequence = 1;
            lastContainerSequence = 5;
        }
        else
        {
            Debug.LogError("Error! " + playerTurn + "is not legal");
            firstContainerSequence = -99;
            lastContainerSequence = -99;
            earnedCoins = "???";

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
                if (playerTurn == "player 1")
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
                    GameObject.Find(earnedCoins).GetComponent<Spawner>().CoinsSpawner();
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

    public int ContainerSequenceGetter(string containerName)
    {
        containerName = containerName.Substring(11);
        int containerSequence = Convert.ToInt16(containerName.Remove(containerName.LastIndexOf(")")));
        return containerSequence;
    }

    public IEnumerator UsingTurn(bool areYouNextContainerChecker, string moveChoice, int containerSequence, int coinsCounter)
    {
        uIController.GetComponent<DisplayOnlyUIController>().selection.SetActive(false);
        isAcceptedToPlay = false;
        isAcceptedToClick = false;

        if (areYouNextContainerChecker == true)
        {
            audioController.PlayingSFX(audioController.pickingCoin);
            yield return new WaitForSeconds(0.5f);
        }

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
            NextContainerChecker(containerSequence + 1, moveChoice);
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
            NextContainerChecker(containerSequence - 1, moveChoice);
        }
    }

    void NextContainerChecker(int nextContainerSequence, string moveChoice)
    {
        List<GameObject> coins = GameObject.Find("Container (" + nextContainerSequence + ")").GetComponent<Counter>().coins;
        if (coins.Count == 0)
        {
            if (moveChoice == "go up")
            {
                if (nextContainerSequence == 12)
                {
                    nextContainerSequence = 0;
                }
                StartCoroutine(EarnedContainerChecker("go up", nextContainerSequence + 1));
            }
            if (moveChoice == "go down")
            {
                if (nextContainerSequence == 1)
                {
                    nextContainerSequence = 13;
                }
                StartCoroutine(EarnedContainerChecker("go down", nextContainerSequence - 1));
            }
        }
        else if (nextContainerSequence == 6 || nextContainerSequence == 12)
        {
            StartCoroutine(PlayerTurnChanger());
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
            isPlaying = false;

            StartCoroutine(UsingTurn(true, moveChoice, nextContainerSequence, n));

        }
    }
}