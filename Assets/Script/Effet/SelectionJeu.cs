using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectBoard", menuName = "ScriptableObjects/ScriptableAction/SelectBoard", order = 3)]
public class SelectionJeu : ActionScript
{
    public override void PlayAction(int x)
    {
        FightManager.Instance.SelectOnGame(x);
        FightManager.Instance.Action();
    }
}
