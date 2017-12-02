using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MaterialUI;

public class Spellbook : MonoBehaviour{
    private const int spellbookSize = 4;
    public List<Spell> spells = new List<Spell>(SpellbookSize);
    public string[] names;

    #region Properties
    public static int SpellbookSize
    {
        get
        {
            return spellbookSize;
        }
    }

    public List<Spell> Spells
    {
        get
        {
            return spells;
        }

        set
        {
            spells = value;
        }
    }
#endregion

    public bool CheckIntegrity()
    {
        bool integrity = true;
        if (IsRedundancy())
        {
            integrity = false;
            DialogManager.ShowAlert("Spellbook redundancy", "Equip Integrity Error", MaterialIconHelper.GetIcon(MaterialIconEnum.DATA_USAGE));
        }
        if (!IsAppropriateSize())
        {
            integrity = false;
            DialogManager.ShowAlert("Spellbook size", "Equip Integrity Error", MaterialIconHelper.GetIcon(MaterialIconEnum.DATA_USAGE));
        }
        return integrity;
    }

    // TODO Configure which algorithm is better. 
    private bool IsRedundancy()
    {
        bool redundancy;
        // redundancy = Spells.GroupBy(n => n).Any(c => c.Count() > 1);
        redundancy = spells.Distinct().Count() != spells.Count();
        return redundancy;
    }

    private bool IsAppropriateSize()
    {
        return Spells.Count() == SpellbookSize;
    }

    public new string ToString()
    {
        string text = "Spellbook: ";
        foreach (Spell spell in spells)
        {
            text += spell.SpellName;
            text += ", ";
        }
        text += "\n";
        return text;
    }

    void Update()
    {
        names = new string[spells.Count];
        for (int i = 0; i < spells.Count; ++i)
            names[i] = spells[i].SpellName;
    }
}
