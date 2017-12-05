using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public RawImage rawImage;
    public Text text;
    public Text infoText;
    public Spell spell;

    [SerializeField] private SpellbookUI spellbookUI;

    private const float centerLimit = 6.0f;

    public void Init(Spell spell)
    {
        this.spell = spell;
        const string path = "SkillIcons/";
        rawImage.texture = Resources.Load(path + spell.SpellName, typeof(Texture2D)) as Texture2D;
        text.text = spell.SpellName;
        infoText = GameObject.Find("SpellInfo").transform.GetChild(1).GetComponent<Text>();
        spellbookUI = GameObject.Find("EquipUI").GetComponent<SpellbookUI>();
    }

    void Update()
    {
        if (transform.position.x < centerLimit && transform.position.x > -centerLimit)
        {
            if (infoText)
            {
                infoText.text = spell.ToString();
            }
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
            transform.localScale = new Vector3(.8f, .8f, .8f);
    }
}
