using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectHand", menuName = "ScriptableObjects/ScriptableAction/SelectHand", order = 3)]
public class SelectionMain : ActionScript
{
    public override void PlayAction(int x)
    {
        FightManager.Instance.SelectOnHand(x);
        FightManager.Instance.Action();
    }
}
