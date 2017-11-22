using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFactory : MonoBehaviour {
    public Transform parent;
    public SpellManager spellManager;

    public SpellInstance CreateSpell(Spell spell)
    {
        GameObject gameObj = new GameObject();
        gameObj.transform.SetParent(parent);
        gameObj.name = "Spell:" + spell.SpellName;
        SpellInstance spellInstance = gameObj.AddComponent<SpellInstance>();
        spellInstance.Init(spell);
        spellInstance.spellManager = spellManager;
        gameObj.AddComponent<SpellCooldown>().Init();
        return spellInstance;
    }
}
