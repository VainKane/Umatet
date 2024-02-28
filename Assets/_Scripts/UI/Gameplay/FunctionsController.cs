using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionsController : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject settingButton;

    private GameMechanic gameMechanic;

    // Start is called before the first frame update
    void Start()
    {
        settingPanel.SetActive(false);
        settingButton.SetActive(true);

        gameMechanic = GameObject.Find("GameController").GetComponent<GameMechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UsingSettingPanel()
    {
        settingButton.SetActive(false);
        settingPanel.SetActive(true);
    }
    public void ClosingSettingPanel()
    {
        settingButton.SetActive(true);
        settingPanel.SetActive(false);
    }

    public void ReturningToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        SceneManager.LoadScene(1);

    }

    public void SavingData()
    {
        for (int i = 1; i <= 12; i++)
        {
            PlayerPrefs.SetInt("Container (" + i + ")'s coinsCounter", GameObject.Find("Container (" + i + ")").GetComponent<Counter>().coins.Count);
        }
        PlayerPrefs.SetInt("Container (b)'s coinsCounter", GameObject.Find("Container (b)").GetComponent<Counter>().coinsCounter);
        PlayerPrefs.SetInt("Container (c)'s coinsCounter", GameObject.Find("Container (c)").GetComponent<Counter>().coinsCounter);
        
        PlayerPrefs.SetInt("player1Loan", gameMechanic.player1Loan);
        PlayerPrefs.SetInt("player2Loan", gameMechanic.player2Loan);

        PlayerPrefs.SetString("playerTurn", gameMechanic.playerTurn);

        PlayerPrefsExtra.SetBool("isRedEnvelope1Hiden", gameMechanic.isRedEnvelope1Hiden);
        PlayerPrefsExtra.SetBool("isRedEnvelope2Hiden", gameMechanic.isRedEnvelope2Hiden);

        Debug.Log("data saved!");

    }
}
