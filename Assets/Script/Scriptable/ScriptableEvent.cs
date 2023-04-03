using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "ScriptableObjects/ScriptableEvent", order = 4)]
public class ScriptableEvent : ScriptableObject
{
    [SerializeField] public Sprite EventImage;
    [SerializeField] public string Name;
    [SerializeField] public ScriptableAction Action;
}
