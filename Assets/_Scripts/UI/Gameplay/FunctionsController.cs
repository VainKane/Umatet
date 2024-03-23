using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionsController : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject settingButton;
    [SerializeField] private GameObject notification;

    private GameMechanic gameMechanic;

    // Start is called before the first frame update
    void Start()
    {
        settingButton.SetActive(true);
        gameMechanic = GameObject.Find("GameController").GetComponent<GameMechanic>();
    }


    public void UsingSettingPanel()
    {
        settingButton.SetActive(false);
        settingPanel.SetActive(true);

        GameObject.Find("SettingPanel").GetComponent<FadableObjects>().FadeIn();
    }
    public void ClosingSettingPanel()
    {
        settingButton.SetActive(true);
        GameObject.Find("SettingPanel").GetComponent<FadableObjects>().FadeOut();



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

        StartCoroutine(Notify());
    }

    private IEnumerator Notify()
    {
        notification.SetActive(true);
        notification.GetComponent<FadableObjects>().FadeIn();
        yield return new WaitForSeconds(Mathf.PI);
        notification.GetComponent<FadableObjects>().FadeOut();
    }
}
