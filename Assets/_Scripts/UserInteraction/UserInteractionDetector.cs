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
        if (gameController.GetComponent<EventController>().isAcceptedToPlay == true)
        {
            if (Input.GetKeyDown(gameController.GetComponent<EventController>().keyIncreasingContainerSequence))
            {
                gameController.GetComponent<EventController>().isAcceptedToPlay = false;
                StartCoroutine(gameController.GetComponent<EventController>().UsingTurn("go up", gameController.GetComponent<EventController>().ContainerSequenceCalculator(gameController.GetComponent<EventController>().container.name), gameController.GetComponent<EventController>().coinsCounter));
            }
            if (Input.GetKey(gameController.GetComponent<EventController>().keyDecreasingContainerSequence))
            {
                gameController.GetComponent<EventController>().isAcceptedToPlay = false;
                StartCoroutine(gameController.GetComponent<EventController>().UsingTurn("go down", gameController.GetComponent<EventController>().ContainerSequenceCalculator(gameController.GetComponent<EventController>().container.name), gameController.GetComponent<EventController>().coinsCounter));
            }
        }
    }
}
