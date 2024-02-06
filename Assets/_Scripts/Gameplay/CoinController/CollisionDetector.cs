using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollisionDetector : MonoBehaviour
{
    private bool isCounted;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        isCounted = false;
        

    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name != "Coin(Clone)" && isCounted == false)
        {
            GameObject.Find(collision.name).GetComponent<Counter>().coins.Add(obj); 
            isCounted = true;

        }
    }
}
