using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipParser : MonoBehaviour
{
    public TextAsset equipFile;
    public Equipment equipment;

    public Equipment Init()
    {
        string[] pairs = equipFile.text.Split(' ');
        List<int> list = new List<int>();
        foreach(string pair in pairs)
        {
            int item;
            if (int.TryParse(pair, out item))
                list.Add(item);
        }
        if (list.Count != 9)
            Debug.Log("List count is not 9!!");
        equipment = Instantiate(equipment, GameObject.Find("Data").transform);
        equipment.name = "Equipment";
        equipment.Init(list);
        return equipment;
    }
}
