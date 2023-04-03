using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnHandCard : MonoBehaviour
{
    public int numberCard;

    public void Click()
    {
        Debug.Log("je clique sur la carte hand");
        Card reference = HandDeck.Instance.CardInHand[numberCard];
        if (reference.GetType().Equals(typeof(Mob)))
        {
            Mob mobRef = (Mob)reference;
            mobRef.Click();
        }
        else
        {
            Event eventRef = (Event)reference;
            eventRef.Click();
        }
        
    }
}
