using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadableObjects : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private bool isFadedIn;
    private bool isFadedOut;

    private GameObject obj;

    private void Start()
    {
        obj = gameObject;

        obj.SetActive(false);
        canvasGroup = obj.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (isFadedIn == true)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime * 5f;
                if (canvasGroup.alpha >= 1)
                {
                    isFadedIn = false;
                }
            }
        }
        if (isFadedOut == true)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * 5f;
                if (canvasGroup.alpha <= 0)
                {
                    isFadedOut = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void FadeIn()
    {
        canvasGroup.alpha = 0f;
        isFadedIn = true;
    }

    public void FadeOut()
    {
        isFadedOut = true;
    }
}
