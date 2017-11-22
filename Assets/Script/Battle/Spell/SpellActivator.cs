using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellActivator : MonoBehaviour {
    public enum Flow { Ready, SelectArea, Activate }
    public static Flow flow;
    public static Spell spell;
    public LineRenderer line;

    public void SelectSpell(Spell spell)
    {

    }

    private void CreateLine()
    {

    }
}
