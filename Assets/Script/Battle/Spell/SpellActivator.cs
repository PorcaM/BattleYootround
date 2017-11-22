using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellActivator : MonoBehaviour {
    public enum Flow { Ready, SelectArea, Activate }
    public Flow flow;
    public SpellInstance selectedSpell;
    public SpellArea areaPrefab;
    public SpellArea createdArea;
    public SpellManager spellManager;

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
        createdArea.transform.localScale = new Vector3(r, 1.0f, r);
        flow = Flow.SelectArea;
    }
    
    public void ActivateSpell()
    {
        if (flow == Flow.SelectArea)
        {
            HandleSpellActivate();
            selectedSpell.Activated();
            Destroy(createdArea.gameObject);
            flow = Flow.Ready;
        }
    }

    private void HandleSpellActivate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        Debug.Log(createdArea.transform.position);
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
