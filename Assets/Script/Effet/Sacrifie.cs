using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sacrifie", menuName = "ScriptableObjects/ScriptableAction/Sacrifie", order = 3)]
public class Sacrifie : ActionScript
{
    public override void PlayAction(int x)
    {
        Card currentCard;
        for (int i = FightManager.Instance.selectedCard.Count-1; i >= FightManager.Instance.selectedCard.Count-x; i--)
        {
            currentCard = FightManager.Instance.selectedCard[i];
            if (currentCard.IsOnAttack)
            {
                BoardManager.Instance.RemoveCardInBoard((Mob)currentCard);
            }
            else
                HandDeck.Instance.RemoveCardInHand(currentCard);
        }
        FightManager.Instance.ResetSelection();
        FightManager.Instance.Action();
    }
}
