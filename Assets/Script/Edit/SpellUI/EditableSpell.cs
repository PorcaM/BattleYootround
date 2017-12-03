using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class EditableSpell : MonoBehaviour {
    public SpellbookUI spellbookUI;
    public bool upside;
    public TabView tabView;

    private Vector3 downPosition;
    private float dragMinDistance = 100.0f;
    private const int spellPage = 0;

    public void Init(SpellbookUI spellbookUI, bool upside)
    {
        this.spellbookUI = spellbookUI;
        this.upside = upside;
        tabView = GameObject.Find("Tab View").GetComponent<TabView>();
    }

    void OnMouseDown()
    {
        if (tabView.currentPage == spellPage)
            downPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        if (tabView.currentPage == spellPage)
        {
            Spell spell = GetComponent<SpellUI>().spell;
            Vector3 upPosition = Input.mousePosition;
            float delta = upPosition.y - downPosition.y;
            if (delta > dragMinDistance && upside)
            {
                Debug.Log("up");
                spellbookUI.Add(spell);
            }
            if (delta < -dragMinDistance && !upside)
            {
                Debug.Log("down");
                spellbookUI.Remove(GetComponent<SpellUI>());
            }
        }
    }
}
