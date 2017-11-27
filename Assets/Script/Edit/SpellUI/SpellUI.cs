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

    private Vector3 downPosition;
    private float dragMinDistance = 100.0f;

    [SerializeField] private SpellbookUI spellbookUI;

    public void Init(Spell spell)
    {
        this.spell = spell;
        const string path = "SkillIcons/";
        rawImage.texture = Resources.Load(path + spell.SpellName, typeof(Texture2D)) as Texture2D;
        text.text = spell.SpellName;
        infoText = GameObject.Find("SpellInfo").transform.GetChild(0).GetComponent<Text>();
        spellbookUI = GameObject.Find("EquipUI").GetComponent<SpellbookUI>();
    }

    void Update()
    {
        if (transform.position.x < 6 && transform.position.x > -6)
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

    void OnMouseDown()
    {
        downPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Vector3 upPosition = Input.mousePosition;

        float delta = upPosition.y - downPosition.y;
        if (delta > dragMinDistance)
        {
            Debug.Log("up");
            spellbookUI.Add(spell);
        }
        if (delta < -dragMinDistance)
        {
            Debug.Log("down");
            spellbookUI.Remove(spell);
        }
    }
}
