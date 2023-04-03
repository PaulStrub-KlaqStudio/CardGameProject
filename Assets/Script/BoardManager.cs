using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] CardUI[] MobCards;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddCardToBoard(Card cardToPlay)
    {
        if (cardToPlay.GetType().Equals(typeof(Mob)))
        {
            for(int i = 0; i < FightManager.Instance.mobInGame.Length; i++)
            {
                if (FightManager.Instance.mobInGame[i] == null)
                {
                    FightManager.Instance.mobInGame[i] = (Mob)cardToPlay;
                    cardToPlay.IsOnAttack = true;
                    MobCards[i].gameObject.SetActive(true);
                    MobCards[i].InitCardMobBoard((Mob)cardToPlay);
                    return;
                }
            }
        }
    }

    public void RemoveCardInBoard(Mob cardPlayed)
    {
        Mob[] mobsInGame = FightManager.Instance.mobInGame;
        for (int i = 0; i < mobsInGame.Length - 1; i++)
        {
            if (mobsInGame[i] == cardPlayed)
            {
                mobsInGame[i] = null;
                MobCards[i].gameObject.SetActive(false);
            }
        }
    }
}
