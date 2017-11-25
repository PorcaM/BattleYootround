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
            (horse.IsEnemyWith(field.Guest(0))) ? "battle" : "together";
        return previewName;
    }

    public static void Translate(Horse horse, YootGame.YootCount yootCount)
    {
        HandleBeginner(horse);
        YootField curr = horse.currField;
        YootField dest = YootBoard.GetDestination(horse, yootCount);
        curr.Leave(horse);
        const float animTime = 1.0f;
        AnimateHorse(horse, dest.transform, animTime);
        if (dest.IsGoal())
            horse.GoalIn();
        else
            dest.Arrive(horse);
    }

    private static void HandleBeginner(Horse horse)
    {
        if (horse.IsStandby())
            horse.horseManager.NewStandbyRunner();
    }

    private static void AnimateHorse(Horse horse, Transform dest, float time)
    {
        horse.gameObject.AddComponent<HorseAnimator>().
            Init(horse.transform.position, dest.position, time);
    }
}
