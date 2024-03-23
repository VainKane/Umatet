using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private GameObject obj;
    private GameMechanic gameMechanic;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        gameMechanic = GameObject.Find("GameController").GetComponent<GameMechanic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (gameMechanic.isAcceptedToClick == true)
        {
            if (obj.GetComponent<Counter>().coins.Count != 0)
            {
                if (obj.name == "Container (7)" || obj.name == "Container (8)" || obj.name == "Container (9)" || obj.name == "Container (10)" || obj.name == "Container (11)")
                {
                    if (gameMechanic.playerTurn == "player 2")
                    {
                        if (PlayerPrefsExtra.GetBool("isOnBot") == false)
                        {
                            gameMechanic.ReceiveClicks(obj);
                        }
                    }
                }
                if (obj.name == "Container (1)" || obj.name == "Container (2)" || obj.name == "Container (3)" || obj.name == "Container (4)" || obj.name == "Container (5)")
                {
                    if (gameMechanic.playerTurn == "player 1")
                    {
                        gameMechanic.ReceiveClicks(obj);
                    }
                }
            }
        }
    }
}
