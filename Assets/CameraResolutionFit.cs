using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolutionFit : MonoBehaviour
{
    private float minSize;
    private float maxSize;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        minSize = 9f;
        maxSize = 16f;

        mainCamera = Camera.main;

        MainCameraRatioFixing();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void MainCameraRatioFixing()
    {
        float targetAspect = 16f / 9f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = mainCamera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            mainCamera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = mainCamera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            mainCamera.rect = rect;
        }
    }
}
