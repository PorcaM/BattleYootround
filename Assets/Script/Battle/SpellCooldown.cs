using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCooldown : MonoBehaviour {
    public SpellInstance spellInstance;
    public UnityEngine.UI.Image image;

    void Update()
    {
        float ratio = spellInstance.Ratio;
        image.fillAmount = 1 - ratio;
    }
}
