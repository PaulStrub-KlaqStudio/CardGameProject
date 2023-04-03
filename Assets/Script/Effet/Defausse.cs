using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defausse", menuName = "ScriptableObjects/ScriptableAction/Defausse", order = 3)]
public class Defausse : ActionScript
{
    public override void PlayAction(int x)
    {
        
        for(int i = 0; i < x; i++)
        {
            DeckManager.Instance.DrawCard();
        }
        FightManager.Instance.Action();
    }
}
