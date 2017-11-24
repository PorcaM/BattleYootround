using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellActivator : MonoBehaviour {
    public enum Flow { Ready, SelectArea }
    public Flow flow;
    public SpellInstance selectedSpell;
    public SpellArea areaPrefab;
    public SpellArea createdArea;
    public SpellManager spellManager;

    public void Cleanup()
    {
        if(createdArea)
            Destroy(createdArea.gameObject);
        flow = Flow.Ready;
    }

    public void SelectSpell(SpellInstance spell)
    {
        if (flow == Flow.SelectArea)
        {
            Destroy(createdArea.gameObject);
        }
        selectedSpell = spell;
        ShowArea();
    }

    private void ShowArea()
    {        
        createdArea = Instantiate(areaPrefab, transform) as SpellArea;
        createdArea.spellActivator = this;
        createdArea.radius = selectedSpell.radius;
        const float realSizeFactor = 0.2f;
        float r = realSizeFactor * selectedSpell.radius;
        createdArea.transform.GetChild(0).localScale = new Vector3(r, 1.0f, r);
        flow = Flow.SelectArea;
    }
    
    public void ActivateSpell()
    {
        if (flow == Flow.SelectArea)
        {
            ApplySpellEffect();
            selectedSpell.Activated();
            Destroy(createdArea.gameObject);
            flow = Flow.Ready;
        }
    }

    private void ApplySpellEffect()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, createdArea.transform.position);
            if (distance <= selectedSpell.radius)
            {
                enemy.GetComponent<UnitInstance>().UnderAttack(selectedSpell.damage);
            }
        }
    }
}
