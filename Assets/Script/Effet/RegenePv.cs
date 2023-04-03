using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regene", menuName = "ScriptableObjects/ScriptableAction/Regene", order = 3)]
public class RegenePv : ActionScript
{
    public override void PlayAction(int x)
    {
        if (FightManager.Instance.selectedCard[FightManager.Instance.selectedCard.Count - 1].GetType().Equals(typeof(Mob)))
        {
            Mob toRegeneMob = (Mob)FightManager.Instance.selectedCard[FightManager.Instance.selectedCard.Count - 1];
            toRegeneMob.Regene(x);
        }
        else
        {
            FightManager.Instance.mobPlaying.Regene(x);
        }
        FightManager.Instance.ResetSelection();
        FightManager.Instance.Action();
    }
}
