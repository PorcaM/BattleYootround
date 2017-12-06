using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class SpellbookUIController : MonoBehaviour {
    public Spellbook spellbook;
    public Transform spellbookUI;

    public void Apply()
    {
        Debug.Log(spellbook.spells.Count);
        for(int i = 0; i < spellbook.spells.Count; ++i)
        {
            Spell spell = spellbook.spells[i];
            RawImage rawImage = spellbookUI.GetChild(i).GetChild(1).GetComponent<RawImage>();
            const string path = "SkillIcons/";
            rawImage.texture = Resources.Load(path + spell.SpellName, typeof(Texture2D)) as Texture2D;
            Text text = spellbookUI.GetChild(i).GetChild(1).GetComponent<Text>();
            text.text = spell.SpellName;
        }
    }
}
