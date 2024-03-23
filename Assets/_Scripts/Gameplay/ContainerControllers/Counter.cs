using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Counter : MonoBehaviour
{
    public Text txtCoinsCounter;
    public List<GameObject> coins;

    internal int coinsCounter;
    private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        txtCoinsCounter.text = Convert.ToString(coins.Count);

    }
    // Update is called once per frame
    void Update()
    {
        if (obj.name != "Container (b)" && obj.name != "Container (c)")
        {
            txtCoinsCounter.text = Convert.ToString(coins.Count);
        }
        else
        {
            txtCoinsCounter.text = Convert.ToString(coinsCounter);
        }
    }


}
