using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour {
    [SerializeField]
    private SpellInstance spellInstance;

    public void Init(SpellInstance instance)
    {
        spellInstance = instance;
        InitIconTexture();
    }
    
    private void InitIconTexture()
    {
        RawImage iconImage = transform.GetChild(0).GetComponent<RawImage>();
        const string path = "SkillIcons/";
        iconImage.texture = Resources.Load(path + spellInstance.spellName, typeof(Texture2D)) as Texture2D;
    }

    public void ActivateSpell()
    {
        spellInstance.Activate();
    }
}
