using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour {
    public SpellInstance spellInstance;
    public Image image;

    public void Init()
    {
        spellInstance = GetComponent<SpellInstance>();
    }

    void Update()
    {
        float ratio = spellInstance.Ratio;
        image.fillAmount = ratio;
    }

    public void ActivateSpell()
    {
        spellInstance.Activate();
    }
}
