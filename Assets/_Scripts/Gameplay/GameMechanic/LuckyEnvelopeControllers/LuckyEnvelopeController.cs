using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyEnvelopeController : MonoBehaviour
{
    private int LuckyEnvelopeValue;

    // Start is called before the first frame update
    void Start()
    {
        LuckyEnvelopeValue = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
