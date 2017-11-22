using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour {
    public void Init(string spellName)
    {
        InitIconTexture(spellName);
    }
    
    private void InitIconTexture(string spellName)
    {
        RawImage iconImage = transform.GetChild(0).GetComponent<RawImage>();
        const string path = "SkillIcons/";
        iconImage.texture = Resources.Load(path + spellName, typeof(Texture2D)) as Texture2D;
    }
}
