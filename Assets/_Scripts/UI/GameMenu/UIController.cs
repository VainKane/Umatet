using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private InputField player1Input;
    [SerializeField] private InputField player2Input;
    [SerializeField] private GameObject gameSettingsPanel;
    [SerializeField] private Toggle botToggle;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Text player2Text;
    [SerializeField] private GameObject notification;
    [SerializeField] private Text notificatonText;
    [SerializeField] private Scrollbar tutorialScrollBar;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject player1Character;
    [SerializeField] private GameObject player2Character;
    [SerializeField] private Text player2IconText;


    // Start is called before the first frame update
    void Start()
    {
        gameSettingsPanel.SetActive(false);

        player1Input.characterLimit = 14;
        player2Input.characterLimit = 14;

        tutorialPanel.SetActive(true);
        tutorialScrollBar.value = 1;
        tutorialPanel.SetActive(false);


        if (PlayerPrefs.HasKey("player1Name") == true || PlayerPrefs.HasKey("player2Name") == true)
        {
            player1Input.text = PlayerPrefs.GetString("player1Name");
            player2Input.text = PlayerPrefs.GetString("player2Name");
        }

        CheckingPlayer2();

    }

    // Update is called once per frame

    public void Playing()
    {
        gameSettingsPanel.SetActive(true);
        gameSettingsPanel.GetComponent<FadableObjects>().FadeIn();
    }


    public void PlayingSavedGame()
    {
        if (PlayerPrefs.HasKey("playerTurn") == true)
        {
            if (player1Input.text.Length > 0 && player2Input.text.Length > 0 && PlayerPrefs.HasKey("playerTurn"))
            {
                DataSaver();
                PlayerPrefsExtra.SetBool("isPlayingSavedGame", true);
                SceneManager.LoadScene(1);
            }
            else
            {
                StartCoroutine(Notify(false));
            }
        }
        else
        {
            StartCoroutine(Notify(true));
        }
    }

    public void PlayingNewGame()
    {
        if (player1Input.text.Length > 0 && player2Input.text.Length > 0)
        {
            DataSaver();
            PlayerPrefsExtra.SetBool("isPlayingSavedGame", false);
            SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine(Notify(false));
        }
    }

    private IEnumerator Notify(bool isPlaySavedGame)
    {
        if (isPlaySavedGame == false)
        {
            if (player1Input.text.Length > 0)
            {
                notificatonText.text = "Please type player 2's name";
            }
            else
            {
                notificatonText.text = "Please type player 1's name";
            }
        }
        else
        {
            notificatonText.text = "Cannot find any saves!";
        }

        notification.SetActive(true);
        notification.GetComponent<FadableObjects>().FadeIn();
        yield return new WaitForSeconds(Mathf.PI);
        notification.SetActive(true);
        notification.GetComponent<FadableObjects>().FadeOut();
    }

    private void DataSaver()
    {
        PlayerPrefs.SetString("player1Name", player1Input.text);
        PlayerPrefs.SetString("player2Name", player2Input.text);
        PlayerPrefsExtra.SetBool("isOnBot", botToggle.isOn);

        player1Character.GetComponent<SpriteManager>().SaveSelectedCharacterSequence();
        player2Character.GetComponent<SpriteManager>().SaveSelectedCharacterSequence();
    }

    public void ClosingGameSettingsPanel()
    {
        gameSettingsPanel.GetComponent<FadableObjects>().FadeOut();
    }

    public void CheckingPlayer2()
    {
        if (botToggle.isOn == false)
        {
            player2Text.text = "Player 2's name:";
            player2IconText.text = "Player 2's name:";
        }
        else
        {
            player2Text.text = "Bot's name:";
            player2IconText.text = "Bot's\nicon: ";
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenTutorialPanel()
    {
        tutorialPanel.SetActive(true);
        tutorialPanel.GetComponent<FadableObjects>().FadeIn();
    }

    public void CloseTutorialPanel()
    {
        tutorialPanel.GetComponent<FadableObjects>().FadeOut();
    }

}
