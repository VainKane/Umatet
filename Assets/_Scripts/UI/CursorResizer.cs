using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorResizer : MonoBehaviour
{
    public float cursorScale = 1.5f; 

    public class CursorResizers : MonoBehaviour
    {
        RectTransform rectTransform;
        public float cursorSize;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            ResizeCursor(cursorSize);
        }
        void ResizeCursor(float newSize)
        {
            rectTransform.sizeDelta = new Vector2(newSize, newSize);
        }
    }
}