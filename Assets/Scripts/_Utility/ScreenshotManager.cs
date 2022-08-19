using UnityEditor;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("Assets/screenshot.png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
