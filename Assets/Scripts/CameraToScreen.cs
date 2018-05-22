/*
 * Corrects the orthographic size in order to fit the scene into the device screen at best
 * 
 * */

using UnityEngine;

public class CameraToScreen : MonoBehaviour {

    float maxPixelWidth = 2048;
    float maxPixelHeight = 1380;

    float assetsAspectRatio;

    [Header("Screen Resolution Test Variables")]
    public float screenPixelWidth;
    public float screenPixelHeight;

    float screenAspectRatio;

    float targetResolution;

    public void Init()
    {
        assetsAspectRatio = maxPixelWidth / maxPixelHeight;

#if !UNITY_EDITOR
            screenPixelWidth = Screen.width;
            screenPixelHeight = Screen.height;
#endif

        screenAspectRatio = screenPixelWidth / screenPixelHeight;

        if (assetsAspectRatio > screenAspectRatio) // assets are wider than the screen
        {
            Camera.main.rect = new Rect(0, 0, maxPixelHeight * screenAspectRatio, maxPixelHeight);
            targetResolution = maxPixelHeight;
        }
        else if (assetsAspectRatio < screenAspectRatio) // assets are taller than the screen
        {
            Camera.main.rect = new Rect(0, 0, maxPixelWidth , maxPixelWidth / screenAspectRatio);
            targetResolution = maxPixelWidth / screenAspectRatio;
        }
        else // assets fits the screen
        {
            Camera.main.rect = new Rect(0, 0, maxPixelWidth, maxPixelHeight);
            targetResolution = maxPixelHeight;
        }

        // according to unity documentation the dividing factor should be 200, but I found a better fit with 180
        Camera.main.orthographicSize = targetResolution / 180; 
    }
}
