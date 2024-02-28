using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollisionDetector : MonoBehaviour
{
    private bool isCounted;
    GameObject obj;

    void Start()
    {
        obj = gameObject;
        isCounted = false;
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
