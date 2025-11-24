using UnityEngine;

namespace others.Code
{
    public class Screenshot : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ScreenCapture.CaptureScreenshot("Handout.png");
                Debug.Log("screenshot taken");
            }
        }
    }
}