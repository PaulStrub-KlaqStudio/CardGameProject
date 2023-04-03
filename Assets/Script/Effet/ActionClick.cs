using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionClick : MonoBehaviour
{
    [SerializeField] bool IsOne;
    [SerializeField] public Mob MobReference;

    public void Click()
    {
        MobReference.PlayAction(IsOne);
    }
}