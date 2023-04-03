using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionPioche", menuName = "ScriptableObjects/ScriptableAction/Pioche", order = 3)]
public class Pioche : ActionScript
{
    public override void PlayAction(int x)
    {
        for(int i=0; i < x; i++)
        {
            HandDeck.Instance.AddToHand(DeckManager.Instance.DrawCard());
        }
        FightManager.Instance.Action();
    }
}
