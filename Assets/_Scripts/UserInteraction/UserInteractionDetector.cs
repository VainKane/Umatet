using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteractionDetector : MonoBehaviour
{
    private GameMechanic gameMechanic;

    // Start is called before the first frame update
    void Start()
    {
        gameMechanic = GameObject.Find("GameController").GetComponent<GameMechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMechanic.isAcceptedToPlay == true)
        {
            if (Input.GetKeyDown(gameMechanic.keyIncreasingContainerSequence))
            {
                StartCoroutine(gameMechanic.UseTurn(false, "go up", gameMechanic.GetContainerSequence(gameMechanic.container.name), gameMechanic.coinsCounter));
            }
            if (Input.GetKey(gameMechanic.keyDecreasingContainerSequence))
            {
                StartCoroutine(gameMechanic.UseTurn(false, "go down", gameMechanic.GetContainerSequence(gameMechanic.container.name), gameMechanic.coinsCounter));
            }
        }
    }
}
