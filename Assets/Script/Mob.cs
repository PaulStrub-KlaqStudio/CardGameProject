using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mob : Card
{

    [SerializeField] public ScriptableMob MobScriptable;
    ScriptableAction SOAction1;
    ScriptableAction SOAction2;

    int PV;
    int Stamina;

    public void TakeDamage(int dmg)
    {
        PV -= dmg;
        if(PV<=0)
            Death();
    }

    public void Click()
    {
        Debug.Log("je suis clické");
        if ((int)FightManager.GameState == 0 && IsOnAttack == false && FightManager.Instance.mobInGame.Length<=3)
        {
            IsOnAttack = true;
            BoardManager.Instance.AddCardToBoard(this);
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

    public void Death()
    {
        if(FightManager.Instance.mobInGame.Length==0 && DeckManager.Instance.Deck.Count == 0 && HandDeck.Instance.CardInHand.Length==0)
        {
            GameManager.Instance.LoseGame();
        }
        BoardManager.Instance.RemoveCardInBoard(this);
    }

    private void Exhaust(int exhaust)
    {
        Stamina -= exhaust;
        if (Stamina <= 0)
            Death();
    }

    public void Regene(int regene)
    {
        PV += regene;
    }

    public override void PlayAction(bool isOne)
    {
        if (isOne)
        {
            if (SOAction1 == null)
            {
                SOAction1 = MobScriptable.action1;
            }
            FightManager.Instance.Action(this, SOAction1);
            Exhaust(SOAction1.exhaust);
        }
        else
        {
            if (SOAction2 == null)
            {
                SOAction2 = MobScriptable.action2;
            }
            FightManager.Instance.Action(this, SOAction2);
            Exhaust(SOAction2.exhaust);
        }
    }

}
