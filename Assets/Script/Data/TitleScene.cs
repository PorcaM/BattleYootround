using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour {
    public XMLParsing xmlParsing;
    public EquipParser equipParser;
    public DataCreator dataCreator;

    void Start()
    {
        xmlParsing.Init();
        dataCreator.Create();
        Equipment equip = equipParser.Init();
        Debug.Log(equip.ToString());
    }

    public void TempInit()
    {
        xmlParsing.Init();
        dataCreator.Create();
        Equipment equip = equipParser.Init();
        Debug.Log(equip.ToString());
    }
}
