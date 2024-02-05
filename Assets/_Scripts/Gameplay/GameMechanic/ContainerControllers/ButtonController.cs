using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private GameObject obj;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WhenClicking()
    {
        if (obj.name == "Container (7)" || obj.name == "Container (8)" || obj.name == "Container (9)" || obj.name == "Container (10)" || obj.name == "Container (11)")

            if (gameController.GetComponent<EventController>().playerTurn == "player 2")
            {
                if (gameController.GetComponent<EventController>().isPlaying == false)
                {
                    if (gameController.GetComponent<EventController>().isAcceptedToClick == true)
                    { 
                        gameController.GetComponent<EventController>().Player2ClickReceiver(obj);
                    }
                }
            }
        if (obj.name == "Container (1)" || obj.name == "Container (2)" || obj.name == "Container (3)" || obj.name == "Container (4)" || obj.name == "Container (5)")
        {
            if (gameController.GetComponent<EventController>().playerTurn == "player 1")
            {
                if (gameController.GetComponent<EventController>().isPlaying == false)
                {
                    if (gameController.GetComponent<EventController>().isAcceptedToClick == true)
                    {
                        gameController.GetComponent<EventController>().Player1ClickReceiver(obj);
                    }
                }
            }

        }
    }
}
