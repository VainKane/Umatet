using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    private float buttonPosX;
    private float buttonPosY;

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    private float radius;

    [HideInInspector] static public bool isTheFirstTimePlaying;
    [HideInInspector] static public bool isPlayingSavedGame;

    [SerializeField] private GameObject coinPrefab;
    private AudioController audioController;

    Vector2 coinPosition;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {


        isTheFirstTimePlaying = true;

        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

        obj = gameObject;

        buttonPosX = obj.transform.position.x;
        buttonPosY = obj.transform.position.y;

        radius = 65f;

        minX = buttonPosX - radius;
        maxX = buttonPosX + radius;
        minY = buttonPosY - radius;
        maxY = buttonPosY + radius;

        isPlayingSavedGame = PlayerPrefsExtra.GetBool("isPlayingSavedGame");

        if (obj.name == "Container (b)" || obj.name == "Container (c)")
        {
            minX = buttonPosX - 150;
            maxX = buttonPosX + 150;
            minY = buttonPosY - 50;
            maxY = buttonPosY + 50;
        }
        else if (obj.name == "Container (6)" || obj.name == "Container (12)")
        {
            minX = buttonPosX - 55;
            maxX = buttonPosX + 55;
            minY = buttonPosY - 120;
            maxY = buttonPosY + 120;
        }
        else if (isPlayingSavedGame == false)
        {
            for (int i = 0; i < 5; i++)
            {
                CoinsSpawner();
            }
        }
        if (isPlayingSavedGame == true)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(obj.name + "'s coinsCounter"); i++)
            {
                CoinsSpawner();
            }
        }
    }
    public void CoinsSpawner()
    {
        coinPosition = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));

        GameObject uiInstance = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        uiInstance.transform.SetParent(transform);

        if (isTheFirstTimePlaying == false)
        {
            if (obj.name != "Container (b)" && obj.name != "Container (c)")
            {
                audioController.PlayingSFX(audioController.coinDrop);
            }
        }

        obj.GetComponent<Counter>().coinsCounter += 1;
    }

    public void CoinsDestroyer()
    {
        if (obj.name == "Container (b)" || obj.name == "Container (c)")
        {
            Destroy(obj.GetComponent<Counter>().coins[0]);
            obj.GetComponent<Counter>().coinsCounter -= 1;
            obj.GetComponent<Counter>().coins.Remove(obj.GetComponent<Counter>().coins[0]);
        }
        else
        {
            Debug.LogError("Only 'Container (b)' and 'Container (c)' can use 'CoinsEarner'!");
        }

    }
}
