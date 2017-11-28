using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootInitializer : MonoBehaviour {
    public YootBoard yootBoard;
    public PopupPreview popupPreview;
    public Transform previewParent;

    public void Init()
    {
        yootBoard.Init();

        HorseRoute.Init();
        UnitHealthBar.Init();
        PopupPreviewController.Init(popupPreview, previewParent);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
