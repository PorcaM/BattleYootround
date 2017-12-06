using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour {
    public ImageDrawing imageDrawing;
    public ImageSave imageSave;
    public GameObject drawUI;

    public void OnDrawing()
    {
        imageDrawing.enabled = true;
        drawUI.SetActive(true);
    }

    public void OnClear()
    {
        imageSave.Clear();
    }

    public void OnSend()
    {
        imageSave.Save();
        imageSave.Clear();
        imageSave.UploadButton();
        imageDrawing.enabled = false;
        drawUI.SetActive(false);
    }
}
