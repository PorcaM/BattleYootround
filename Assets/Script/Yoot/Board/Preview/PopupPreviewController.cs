using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPreviewController : MonoBehaviour {
    private static PopupPreview popupPreview;
    private static Transform parent;

    public static void Init(PopupPreview popupPreview, Transform parent)
    {
        PopupPreviewController.popupPreview = popupPreview;
        PopupPreviewController.parent = parent;
    }

    public static void CreatePopupPreview(string name, Transform location)
    {
        PopupPreview instance = Instantiate(popupPreview);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(parent, false);
        instance.transform.position = screenPosition;
        instance.Init();
        instance.SetImage(name);
    }
}
