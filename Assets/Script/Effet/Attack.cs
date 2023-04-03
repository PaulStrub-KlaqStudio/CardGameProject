using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "attack", menuName = "ScriptableObjects/ScriptableAction/attack", order = 3)]
public class Attack : ActionScript
{

    public override void PlayAction(int x)
    {
        FightManager.Instance.ennemiesInGame[0].TakeDamage(x);
        FightManager.Instance.Action();
    }
}
