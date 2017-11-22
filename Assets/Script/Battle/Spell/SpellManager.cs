using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour {
    public Equipment equipment;
    public SpellFactory spellFactory;
    public List<SpellInstance> spells;
    public GameObject panel;
    public SpellActivator spellActivator;

    public void Init()
    {
        spells = new List<SpellInstance>();
        spellFactory.parent = transform;
        foreach(Spell spell in equipment.spellbook.spells)
        {
            spells.Add(spellFactory.CreateSpell(spell));
        }
        InitButtons();
    }

    private void InitButtons()
    {
        for (int i = 0; i < 4; ++i)
        {
            SpellButton button = panel.transform.GetChild(i).GetComponent<SpellButton>();
            button.Init(spells[i]);
            spells[i].GetComponent<SpellCooldown>().image = button.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
        }
    }

    public void Select(SpellInstance spell)
    {
        spellActivator.SelectSpell(spell);
    }
}
