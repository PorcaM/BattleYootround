using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class DeckUIController : MonoBehaviour {
    public Deck deck;
    public Transform deckUI;

    public void Apply()
    {

        for (int i = 0; i < deck.units.Count; ++i)
        {
            Unit unit = deck.units[i];
            RawImage rawImage = deckUI.GetChild(i).GetChild(0).GetComponent<RawImage>();
            string path = "UnitImages/profile/";
            rawImage.texture = Resources.Load(path + unit.UnitClass.ToString().ToLower(), typeof(Texture2D)) as Texture2D;
            MaterialButton button = deckUI.GetChild(i).GetChild(1).GetComponent<MaterialButton>();
            button.textText = unit.UnitClass.ToString();
        }
    }
}
