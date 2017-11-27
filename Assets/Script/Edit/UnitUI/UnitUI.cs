using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour {
    public RawImage rawImage;
    public Text text;
    public Text infoText;
    public Unit unit;

    [SerializeField] private DeckUI deckUI;

    private const float centerLimit = 6.0f;

    public void Init(Unit unit)
    {
        this.unit = unit;
        const string path = "UnitIcons/";
        rawImage.texture = Resources.Load(path + unit.UnitClass.ToString(), typeof(Texture2D)) as Texture2D;
        text.text = unit.UnitClass.ToString();
        infoText = GameObject.Find("UnitInfo").transform.GetChild(0).GetComponent<Text>();
        deckUI = GameObject.Find("EquipUI").GetComponent<DeckUI>();
    }

    void Update()
    {
        if (transform.position.x < centerLimit && transform.position.x > -centerLimit)
        {
            if (infoText)
            {
                infoText.text = unit.ToString();
            }
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
            transform.localScale = new Vector3(.8f, .8f, .8f);
    }
}
