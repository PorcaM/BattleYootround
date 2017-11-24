using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseTranslator : MonoBehaviour
{
    public static PopupPreview CreatePreview(Horse horse, YootGame.YootCount yootCount, TurnProcessor turnProcessor)
    {
        YootField dest = YootBoard.GetDestination(horse, yootCount);
        string previewName = GetPreviewName(horse, dest);
        return PopupPreviewController.CreatePopupPreview(previewName, dest.transform, turnProcessor);
    }

    private static string GetPreviewName(Horse horse, YootField field)
    {
        string previewName =
        (field.IsEmpty()) ? "dest" :
            (horse.IsEnemy(field.Guest(0))) ? "battle" : "together";
        return previewName;
    }

    public static void Translate(Horse horse, YootGame.YootCount yootCount)
    {
        YootField curr = horse.currentLocation;
        YootField dest = YootBoard.GetDestination(horse, yootCount);
        curr.Leave(horse);
        if (dest.IsGoal())
            horse.GoalIn();
        else
        {
            dest.Arrive(horse);
            horse.transform.position = dest.transform.position;
        }
    }
}
