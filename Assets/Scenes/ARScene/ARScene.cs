using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ARScene : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public KeyCode screenshotKey;
    private Camera Camera
    {
        get
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }
            return _camera;
        }
    }
    private Camera _camera;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            Capture();
        }
    }

    public void Capture()
    {
        //RenderTexture activeRenderTexture = RenderTexture.active;
        //Debug.Log(_camera);
        //RenderTexture.active = _camera.targetTexture;

        //_camera.Render();

        //Texture2D image = new Texture2D(_camera.targetTexture.width, _camera.targetTexture.height);
        //image.ReadPixels(new Rect(0, 0, _camera.targetTexture.width, _camera.targetTexture.height), 0, 0);
        //image.Apply();
        //RenderTexture.active = activeRenderTexture;

        //byte[] bytes = image.EncodeToPNG();
        //Destroy(image);

        //Debug.Log(bytes);

        //File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "output.png"), bytes);
        ScreenCapture.CaptureScreenshot("SomeLevel");
    }
}
