using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteractionDetector : MonoBehaviour
{
    [SerializeField] private GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameObject");
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
