using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ImageFader : MonoBehaviour
{

    public void Fade(float from, float to, float time)
    {
        var image = GetComponent<Image>();
        image.canvasRenderer.SetAlpha(from);
        image.CrossFadeAlpha(to, time, true);
    }
}
