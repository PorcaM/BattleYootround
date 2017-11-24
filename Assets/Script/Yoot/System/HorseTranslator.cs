using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseTranslator : MonoBehaviour {
    public static PopupPreview CreatePreview(Horse horse, YootGame.YootCount yootCount, TurnProcessor turnProcessor)
    {
        YootField dest = YootBoard.GetDestination(horse, yootCount);
        string previewName = GetPreviewName(horse, dest);
        return PopupPreviewController.CreatePopupPreview(previewName, dest.transform, turnProcessor);
    }

    private static string GetPreviewName(Horse horse, YootField field)
    {
        string previewName;
        if (field.IsEmpty())
            previewName = "dest";
        else
            previewName = (horse.IsEnemy(field.Guest(0))) ? "battle" : "together";
        return previewName;
    }

    public static void Translate(Horse horse, YootGame.YootCount yootCount)
    {
        horse.Move(yootCount);
    }
}
