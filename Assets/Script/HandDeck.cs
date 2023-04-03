using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDeck : MonoBehaviour
{
    [SerializeField] public Card[] CardInHand;
    [SerializeField] private CardUI[] CardInHandUI;
    public static HandDeck Instance;
    [SerializeField] private int NumberCardHand;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CardInHand = new Card[NumberCardHand];
        }
    }

    public void AddToHand(Card card)
    {
        for (int i = 0; i < CardInHand.Length ; i++)
        {
            if (CardInHand[i] == null)
            {
                CardInHand[i] = card;
                CardInHandUI[i].gameObject.SetActive(true);
                if (card.GetType().Equals(typeof(Mob)))
                {
                    CardInHandUI[i].InitCardMob((Mob)card);
                }
                else
                {
                    CardInHandUI[i].InitCardEvent((Event)card);
                }
                return;
            }
        }
    }

    public void RemoveCardInHand(Card cardPlayed)
    {
        for(int i = 0; i < CardInHand.Length ; i++)
        {
            if (CardInHand[i] == cardPlayed)
            {
                CardInHand[i] = null;
                CardInHandUI[i].gameObject.SetActive(false);
            }
        }
    }

    public void StartHand()
    {
        for(int i = 0;i< NumberCardHand; i++)
        {
            AddToHand(DeckManager.Instance.DrawCard());
        }
        
    }
}
