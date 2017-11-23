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

    public static PopupPreview CreatePopupPreview(string name, Transform location, TurnProcessor turnProcessor)
    {
        PopupPreview instance = Instantiate(popupPreview);
        instance.transform.SetParent(parent, false);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        screenPosition.y += 30.0f;
        instance.transform.position = screenPosition;
        instance.Init(turnProcessor);
        instance.SetImage(name);
        return instance;
    }
}
