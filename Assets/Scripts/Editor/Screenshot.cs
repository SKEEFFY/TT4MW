using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{
    public Camera sideCamera;

    private void Start()
    {
        Capture();
    }

    public void Capture()
    {
        RenderTexture rt = new RenderTexture(512, 512, 24);
        sideCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(512, 512, TextureFormat.RGB24, false);
        
        sideCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
        screenShot.Apply();
        
        sideCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/SideImage.png", bytes);
        
        Debug.Log("Изображение сохранено: " + Application.dataPath + "/SideImage.png");
    }
}