using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kill", menuName = "ScriptableObjects/ScriptableAction/Kill", order = 3)]
public class Kill : ActionScript
{
    public override void PlayAction(int x)
    {
        FightManager.Instance.ennemiesInGame[0].Death();
        FightManager.Instance.Action();
    }
}
