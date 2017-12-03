using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPreview : MonoBehaviour {
    private RawImage rawImage;
    public Button button;
    private TurnProcessor turnProcessor;
    public const float lifetime = 2.0f;

    public void Init(TurnProcessor turnProcessor)
    {
        this.turnProcessor = turnProcessor;
        
        rawImage = transform.GetChild(1).GetComponent<RawImage>();
        button = GetComponent<Button>();
        button.onClick.AddListener(turnProcessor.RecvAck);
        Destroy(gameObject, lifetime);

        RawImage bg = transform.GetChild(0).GetComponent<RawImage>();
        string color = "g";
        if (turnProcessor.owner.playerID == 1)
            color = "r";
        bg.texture = Resources.Load("PreviewIcons/" + color + "background", typeof(Texture2D)) as Texture2D;
    }

    public void SetImage(string name)
    {
        const string path = "PreviewIcons/";
        rawImage.texture = Resources.Load(path + name, typeof(Texture2D)) as Texture2D;
    }

    void OnDestroy()
    {
        turnProcessor.RecvPreviewDestroy(this);
    }
}
