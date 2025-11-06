using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PixelPerfectCameraScript : MonoBehaviour
{
    public int targetWidth = 220;
    public int targetHeight = 130;
    public float pixelsPerUnit = 16f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        UpdateCameraSize();
    }

    void UpdateCameraSize()
    {
        float targetAspect = (float)targetWidth / targetHeight;
        float screenAspect = (float)Screen.width / Screen.height;

        float orthographicSize = targetHeight / (2f * pixelsPerUnit);

        if (screenAspect >= targetAspect)
        {
            // Bildschirm ist breiter → Höhe bleibt, Breite wird erweitert
            cam.orthographicSize = orthographicSize;
        }
        else
        {
            // Bildschirm ist schmaler → Höhe wird angepasst
            float scale = targetAspect / screenAspect;
            cam.orthographicSize = orthographicSize * scale;
        }
    }

    void Update()
    {
        // Optional: Nur bei tatsächlicher Größenänderung neu berechnen
        if (Screen.width != cam.pixelWidth || Screen.height != cam.pixelHeight)
        {
            UpdateCameraSize();
        }
    }
}