using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableUnit : MonoBehaviour {
    public DeckUI deckUI;
    public bool upside;

    private Vector3 downPosition;
    private float dragMinDistance = 100.0f;

    public void Init(DeckUI deckUI, bool upside)
    {
        this.deckUI = deckUI;
        this.upside = upside;
    }

    void OnMouseDown()
    {
        downPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Unit unit = GetComponent<UnitUI>().unit;
        Vector3 upPosition = Input.mousePosition;
        float delta = upPosition.y - downPosition.y;
        if (delta > dragMinDistance && upside)
        {
            Debug.Log("up");
            deckUI.Add(unit);
        }
        if (delta < -dragMinDistance && !upside)
        {
            Debug.Log("down");
            deckUI.Remove(GetComponent<UnitUI>());
        }
    }
}
