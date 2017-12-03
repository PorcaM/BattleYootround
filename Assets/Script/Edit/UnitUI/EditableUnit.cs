using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class EditableUnit : MonoBehaviour {
    public DeckUI deckUI;
    public bool upside;
    public TabView tabView;

    private Vector3 downPosition;
    private float dragMinDistance = 100.0f;
    private const int unitPage = 1;

    public void Init(DeckUI deckUI, bool upside)
    {
        this.deckUI = deckUI;
        this.upside = upside;
        tabView = GameObject.Find("Tab View").GetComponent<TabView>();
    }

    void OnMouseDown()
    {
        if (tabView.currentPage == unitPage)
            downPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        if (tabView.currentPage == unitPage)
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
}
