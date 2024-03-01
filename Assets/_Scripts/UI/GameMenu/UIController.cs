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

    // Start is called before the first frame update
    void Start()
    {
        gameSettingsPanel.SetActive(false);

        player1Input.characterLimit = 14;
        player2Input.characterLimit = 14;

        if (PlayerPrefs.HasKey("player1Name") == true || PlayerPrefs.HasKey("player2Name") == true)
        {
            player1Input.text = PlayerPrefs.GetString("player1Name");
            player2Input.text = PlayerPrefs.GetString("player2Name");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Playing()
    {
        gameSettingsPanel.SetActive(true);
    }

    public void PlayingSavedGame()
    {
        if (player1Input.text.Length != 0 && player2Input.text.Length != 0)
        {
            DataSaver();
            PlayerPrefsExtra.SetBool("isPlayingSavedGame", true);
            SceneManager.LoadScene(1);
        }
    }

    public void PlayingNewGame()
    {
        if (player1Input.text.Length != 0 && player2Input.text.Length != 0)
        {
            DataSaver();
            PlayerPrefsExtra.SetBool("isPlayingSavedGame", false);
            SceneManager.LoadScene(1);
        }
    }

    private void DataSaver()
    {
        PlayerPrefs.SetString("player1Name", player1Input.text);
        PlayerPrefs.SetString("player2Name", player2Input.text);
        PlayerPrefsExtra.SetBool("isOnBot", botToggle.isOn);
    }

    public void ClosingGameSettingsPanel()
    {
        gameSettingsPanel.SetActive(false);
    }

}
