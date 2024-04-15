using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        RescaleCamera();
        material = new Material(Shader.Find("Unlit/GrayScale"));
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (GlobalValues.slowMoFactor == 2)
            Graphics.Blit(source, destination, material);
        else
            Graphics.Blit(source, destination);
    }
    // Update is called once per frame
    void Update()
    {
        RescaleCamera();
    }

    private void RescaleCamera()
    {
        float ScreenSizeX = 0;
        float ScreenSizeY = 0;

        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;

        float targetaspect = 16.0f / 9.0f;
        float windowaspect = (float)Screen.width / Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = Camera.main;

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
