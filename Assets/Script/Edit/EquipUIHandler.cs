using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class EquipUIHandler: MonoBehaviour {
    public bool isInited = false;
    public Equipment equipment;
    public Equipment original;
    public Equipment tempEquip;

    public SpellUIIntializer spellUIIntializer;
    public UnitUIInitializer unitUIInitializer;
    public EquipParser equipParser;
    
    void Update()
    {
        if (!isInited)
        {
            Init();
        }
    }

    public void Init()
    {
        equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
        isInited = true;

        tempEquip = Instantiate(original);
        tempEquip.Init(equipment);

        spellUIIntializer.Init(tempEquip.spellbook);
        unitUIInitializer.Init(tempEquip.deck);
    }

    public void Save()
    {
        if (tempEquip.IsIntegrity())
        {
            Debug.Log("Good");
            DialogManager.ShowAlert("Pass the integrity test!", "Equip", MaterialIconHelper.GetIcon(MaterialIconEnum.DATA_USAGE));
            equipment.Init(tempEquip);
            // equipParser.Save(tempEquip);
            End();
        }
        else
        {
            Debug.Log("Bad");
        }
    }

    public void Cancel()
    {
        End();
    }

    public void End()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Match");
    }
}
