using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "ScriptableObjects/ScriptableMob", order = 1)]
public class ScriptableMob : ScriptableObject
{
    [Header("Stat")]
    [SerializeField] private int PV;
    [SerializeField] private int Stamina;
    [SerializeField] private ScriptableAction Action1;
    [SerializeField] private ScriptableAction Action2;

    [Header("Card")]
    [SerializeField] public Sprite MobImage;
    [SerializeField] public string Name;

    public int pv
    {
        get { return PV; }
        set
        {
            PV = value;
        }   
    }

    public int stamina
    {
        get { return Stamina; }
        set
        {
            Stamina = value;
        }
    }

    public ScriptableAction action1
    {
        get { return Action1; }
        set { Action1 = value; }
    }

    public ScriptableAction action2
    {
        get { return Action2; }
        set { Action2 = value; }
    }
}
