using UnityEngine;

public class ObjScreenshotter : MonoBehaviour
{
    [SerializeField] private Camera screenshotCamera;
    [SerializeField] private RenderTexture renderTexture;

    private void Start()
    {
        screenshotCamera.targetTexture = renderTexture;
        screenshotCamera.gameObject.SetActive(false);
    }

    public Texture2D InstantiateAndScreenshot(GameObject prefab)
    {
        Texture2D screenshot;
        GameObject obj = Instantiate(prefab);
        screenshot = Screenshot(obj);
        Destroy(obj);
        return screenshot;
    }

    public Texture2D Screenshot(GameObject obj)
    {
        screenshotCamera.gameObject.SetActive(true);
        Vector3 oriPos = obj.transform.position;
        int oriLayer = obj.layer;
        int screenshotLayer = LayerMask.NameToLayer("Screenshot");

        foreach (Transform t in obj.GetComponentsInChildren<Transform>())
            t.gameObject.layer = screenshotLayer;
        obj.layer = screenshotLayer;
        obj.transform.position = new Vector3(screenshotCamera.transform.position.x, screenshotCamera.transform.position.y, screenshotCamera.transform.position.z + 5);
        screenshotCamera.Render();

        RenderTexture.active = renderTexture;
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();
        RenderTexture.active = null;

        foreach (Transform t in obj.GetComponentsInChildren<Transform>())
            t.gameObject.layer = oriLayer;
        obj.layer = oriLayer;
        obj.transform.position = oriPos;
        screenshotCamera.gameObject.SetActive(false);
        return screenshot;
    }
}
