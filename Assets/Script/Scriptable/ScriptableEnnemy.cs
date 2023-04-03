using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "ScriptableObjects/ScriptableEnnemy", order = 2)]
public class ScriptableEnnemy : ScriptableObject
{
    [SerializeField] public int PV;
    [SerializeField] public int Attack;
    [SerializeField] public Sprite Image;
}
