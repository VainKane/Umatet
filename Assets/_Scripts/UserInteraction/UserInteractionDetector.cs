using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteractionDetector : MonoBehaviour
{
    [SerializeField] private GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetComponent<GameMechanic>().isPlaying == false)
        {
            if (gameController.GetComponent<GameMechanic>().isAcceptedToPlay == true)
            {
                if (Input.GetKeyDown(gameController.GetComponent<GameMechanic>().keyIncreasingContainerSequence))
                {
                    StartCoroutine(gameController.GetComponent<GameMechanic>().UsingTurn(false, "go up", gameController.GetComponent<GameMechanic>().ContainerSequenceCalculator(gameController.GetComponent<GameMechanic>().container.name), gameController.GetComponent<GameMechanic>().coinsCounter));
                }
                if (Input.GetKey(gameController.GetComponent<GameMechanic>().keyDecreasingContainerSequence))
                {
                    StartCoroutine(gameController.GetComponent<GameMechanic>().UsingTurn(false, "go down", gameController.GetComponent<GameMechanic>().ContainerSequenceCalculator(gameController.GetComponent<GameMechanic>().container.name), gameController.GetComponent<GameMechanic>().coinsCounter));
                }
            }
        }
    }
}
