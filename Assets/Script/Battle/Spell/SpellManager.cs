using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour {
    public Equipment equipment;
    public SpellFactory spellFactory;
    public List<SpellInstance> spells;
    public GameObject panel;

    public void Init()
    {
        spells = new List<SpellInstance>();
        spellFactory.parent = transform;
        foreach(Spell spell in equipment.spellbook.spells)
        {
            spells.Add(spellFactory.CreateSpell(spell));
        }
        for(int i = 0; i < 4; ++i)
        {
            Transform button = panel.transform.GetChild(i);
            spells[i].GetComponent<SpellCooldown>().image = button.GetComponent<Image>();
            button.GetChild(0).GetComponent<Text>().text = spells[i].spellName;
            button.GetComponent<Button>().onClick.AddListener(spells[i].Activate);
        }
    }
}
