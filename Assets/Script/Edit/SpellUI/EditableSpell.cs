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

    public void Init(SpellbookUI spellbookUI, bool upside)
    {
        this.spellbookUI = spellbookUI;
        this.upside = upside;
        tabView = GameObject.Find("Tab View").GetComponent<TabView>();
    }

    void OnMouseDown()
    {
        downPosition = Input.mousePosition;
    }

    void OnMouseUp()
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
