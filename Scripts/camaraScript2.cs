using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript2 : MonoBehaviour
{
    public int ScreenHight;
    public int ScreenWidth;
    public int targetWidth = 220;   // z. B. 320 Pixel
    public int targetHeight = 130;  // z. B. 180 Pixel
    public float pixelsPerUnit = 16f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        UpdateCameraSize();
    }

    void Update()
    {
        if (Screen.width != targetWidth || Screen.height != targetHeight)
        {
            UpdateCameraSize();
        }
    }

    void UpdateCameraSize()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = (float)targetWidth / targetHeight;
        ScreenHight = Screen.height;
        ScreenWidth = Screen.width;
        if (screenRatio >= targetRatio)
        {
            cam.orthographicSize = targetHeight / (2f * pixelsPerUnit);
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            cam.orthographicSize = targetHeight / (2f * pixelsPerUnit) * differenceInSize;
        }
    }
}

