using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Event : Card
{
    [SerializeField] public ScriptableEvent EventScriptable;
    List<ScriptableAction> ActionList;

    public override void PlayAction(bool IsOn)
    {
        FightManager.Instance.Action(this, EventScriptable.Action);
    }

    public void Click()
    {
        if ((int)FightManager.GameState == 0)
        {
            Debug.Log("je suis clické");
            PlayAction(true);
            HandDeck.Instance.RemoveCardInHand(this);
            HandDeck.Instance.AddToHand(DeckManager.Instance.DrawCard());
        }
        else if ((int)FightManager.GameState == 1)
        {
            Debug.Log("je suis clické");
            FightManager.Instance.AddCard(this);
        }
        else if ((int)FightManager.GameState == 2)
        {
            Debug.Log("je suis clické");
            FightManager.Instance.AddCard(this);
        }
    }

}
