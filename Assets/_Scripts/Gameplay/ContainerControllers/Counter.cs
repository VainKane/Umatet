using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Counter : MonoBehaviour
{
    public Text txtCoinCounter;
    public List<GameObject> coins;

    public int coinsCounter;
    private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        coinsCounter = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (obj.name != "Container (b)" && obj.name != "Container (c)")
        {
            txtCoinCounter.text = Convert.ToString(coins.Count);
        }
        else
        {
            txtCoinCounter.text = Convert.ToString(coinsCounter);
        }
    }
}
