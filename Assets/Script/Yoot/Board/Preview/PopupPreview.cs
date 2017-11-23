using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPreview : MonoBehaviour {
    private RawImage rawImage;

    public void Init()
    {
        rawImage = transform.GetChild(1).GetComponent<RawImage>();
    }

    public void SetImage(string name)
    {
        const string path = "PreviewIcons/";
        rawImage.texture = Resources.Load(path + name, typeof(Texture2D)) as Texture2D;
    }
}
