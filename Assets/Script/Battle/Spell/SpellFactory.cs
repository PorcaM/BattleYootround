using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFactory : MonoBehaviour {
    public Transform parent;

    public SpellInstance CreateSpell(Spell spell)
    {
        GameObject gameObj = new GameObject();
        gameObj.transform.SetParent(parent);
        gameObj.name = "Spell:" + spell.SpellName;
        SpellInstance spellInstance = gameObj.AddComponent<SpellInstance>();
        spellInstance.Init(spell);
        gameObj.AddComponent<SpellCooldown>().Init();
        return spellInstance;
    }
}
