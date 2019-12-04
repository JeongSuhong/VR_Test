using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCamera : ItemBase
{
    public RawImage CameraImage;
    public RawImage PhotoImage;

    public override void Grab(Transform hand)
    {
        transform.parent = hand;
    }

    public override void NonGrab(Vector3 velocity, Vector3 angularVelocity)
    {
        transform.parent = parent;
    }

    public void Capture()
    {
        StartCoroutine(CoCapture());

    }

    private IEnumerator CoCapture()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture renderTex = CameraImage.mainTexture as RenderTexture;
        RenderTexture rendText = RenderTexture.active;
        RenderTexture.active = renderTex;

        // create a new Texture2D with the camera's texture, using its height and width
        Texture2D cameraImage = new Texture2D(renderTex.width, renderTex.height, TextureFormat.RGB24, false);
        cameraImage.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        cameraImage.Apply();
        RenderTexture.active = rendText;

        PhotoImage.texture = cameraImage;
    }
}
