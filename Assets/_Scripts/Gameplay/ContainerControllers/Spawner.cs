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

    static internal bool isTheFirstTimePlaying;

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

        radius = GameObject.Find("Place001").transform.position.x - GameObject.Find("Container (1)").transform.position.x;


        minX = buttonPosX - radius;
        maxX = buttonPosX + radius;
        minY = buttonPosY - radius;
        maxY = buttonPosY + radius;

        if (obj.name == "Container (c)")
        {
            minX = GameObject.Find("Place005").transform.position.x;
            maxX = GameObject.Find("Place004").transform.position.x;
            minY = GameObject.Find("Place005").transform.position.y;
            maxY = GameObject.Find("Place004").transform.position.y;
        }
        else if (obj.name == "Container (b)")
        {
            minX = GameObject.Find("Place006").transform.position.x;
            maxX = GameObject.Find("Place007").transform.position.x;
            minY = GameObject.Find("Place006").transform.position.y;
            maxY = GameObject.Find("Place007").transform.position.y;
        }
        else if (obj.name == "Container (6)" || obj.name == "Container (12)")
        {
            minY = GameObject.Find("Place003").transform.position.y;
            maxY = GameObject.Find("Place002").transform.position.y;
        }
        else if (PlayerPrefsExtra.GetBool("isPlayingSavedGame") == false)
        {
            for (int i = 0; i < 5; i++)
            {
                CoinsSpawner();
            }
        }
        if (PlayerPrefsExtra.GetBool("isPlayingSavedGame") == true)
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
        uiInstance.transform.localScale = new Vector3(0.25f, 0.25f, 0);

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
