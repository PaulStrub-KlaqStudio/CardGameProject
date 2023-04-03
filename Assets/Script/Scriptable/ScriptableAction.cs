using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "ScriptableObjects/ScriptableActionList", order = 3)]
public class ScriptableAction : ScriptableObject
{
    [SerializeField] private List<ActionStruct> ActionList = new List<ActionStruct>();
    [SerializeField] private int Exhaust;
    [TextArea(3, 10)]
    [SerializeField] public string Description;
    [SerializeField] public string Name;

    public void AddAction()
    {
        ActionList.Add(new ActionStruct());
    }

    public void RemoveAction(int i)
    {
        ActionList.Remove(ActionList[i]);
    }

    [System.Serializable]
    public struct ActionStruct
    {
         public ActionScript Action;
         public int ActionValue;
    }

    public List<ActionStruct>  ActionStructList
    {
        get { return ActionList; }
        set { ActionList = value; }
    }

    public List<ActionScript> actionList
    {
        get 
        {
            List<ActionScript> actionListToReturn = new List<ActionScript>();
            foreach(ActionStruct i in ActionList)
            {
                actionListToReturn.Add(i.Action);
            }
            return actionListToReturn;
        }
        set
        {
            actionList = value;
        }
    }

    public List<int> actionInt
    {
        get
        {
            List<int> actionValueListToReturn = new List<int>();
            foreach (ActionStruct i in ActionList)
            {
                actionValueListToReturn.Add(i.ActionValue);
            }
            return actionValueListToReturn;
        }
    }

    public int exhaust
    {
        get { return Exhaust; }
        set { Exhaust = value; }
    }
}
