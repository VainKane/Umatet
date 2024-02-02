using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Counter : MonoBehaviour
{
    public Text txtCoinCounter;
    public List<GameObject> coins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinConterTextManager();
    }

    void CoinConterTextManager()
    {
        txtCoinCounter.text = Convert.ToString(coins.Count);
    }
}
