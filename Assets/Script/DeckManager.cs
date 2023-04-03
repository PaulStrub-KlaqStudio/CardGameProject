using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    public Stack<Card> Deck = new Stack<Card>();
    public List<ScriptableObject> DeckStart;
    [SerializeField] private TextMeshProUGUI NumberCard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GenerateDeck();
    }

    public Card DrawCard()
    {
        if (Deck.Count > 0)
            NumberCard.text = (Deck.Count-1).ToString() ;
            return Deck.Pop();
        return null;
    }

    private void GenerateDeck()
    {
        foreach(ScriptableObject s in DeckStart)
        {
            Debug.Log("je passe");
            if (s.GetType().Equals(typeof(ScriptableMob)))
            {
                Mob toAdd = new Mob();
                toAdd.MobScriptable = (ScriptableMob)s;
                Deck.Push(toAdd);
            }
            else
            {
                Event toAdd = new Event();
                toAdd.EventScriptable = (ScriptableEvent)s;
                Deck.Push(toAdd);
            }
        }
    }
}
