using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{

    public static FightManager Instance;
    [SerializeField] private Mob[] MobInGame = new Mob[3];
     private List<Ennemy> EnnemiesInGame = new List<Ennemy>();
    [SerializeField] private List<ScriptableEnnemy> SOEnnemiesInGame;
    List<ActionScript> ExecutedActionsScript;
    List<int> ExecutedActionInt;
    private List<Card> SelectedCard = new List<Card>();
    private Mob MobPlaying;
    private int indexAction;
    public static State GameState;
    private int CardToSelectedNumber;

    #region getter/setter
    public Mob[] mobInGame
    {
        get { return MobInGame; }
        set { MobInGame = value; }
    }

    public List<Ennemy> ennemiesInGame
    {
        get { return EnnemiesInGame; }
    }

    public List<Card> selectedCard
    {
        get { return SelectedCard; }
    }

    public Mob mobPlaying
    {
        get { return MobPlaying; }
    }

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Ennemy ennemy1 = new Ennemy(SOEnnemiesInGame[0]);
        ennemiesInGame.Add(ennemy1);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameState = State.playerAttack;
        HandDeck.Instance.StartHand();
    }

    public enum State
    {
        playerAttack = 0,
        playerSelectHand = 1,
        playerSelectGame = 2,
        ennemyAttack = 3,
        endFight = 4
    }

    private void AttackEnnemy()
    {
        
        foreach(Ennemy e in ennemiesInGame)
        {
            for (int i = 0; i < mobInGame.Length; i++)
            {
                if (MobInGame[i] != null)
                {
                    MobInGame[i].TakeDamage(e.AttackMob());
                }
            }
        }
        GameState = State.playerAttack;
    }

    public void FightFinish(Ennemy ennemyDied)
    {
        Debug.Log("fin de combat ? ");
        for (int i = 0; i<EnnemiesInGame.Count; i++)
        {
            if (EnnemiesInGame[i] == ennemyDied)
            {
                Debug.Log("removed");
                EnnemiesInGame.Remove(ennemyDied);
            }
        }
        if (EnnemiesInGame.Count <= 0)
        {
            Debug.Log("fin de jeu");
            GameState = State.endFight;
            GameManager.Instance.EndFight();
        }
    }

    #region fightAction

    public void Action()
    {
        indexAction++;
        if (indexAction < ExecutedActionsScript.Count)
        {
            ExecutedActionsScript[indexAction].PlayAction(ExecutedActionInt[indexAction]);
        }
        else
        {
            ResetSelection();
            ExecutedActionInt = null;
            ExecutedActionsScript = null;
            GameState = State.ennemyAttack;
            AttackEnnemy();
        }
    }

    public void Action(Mob mob, ScriptableAction actionsScript)
    {
        MobPlaying = mob;
        ResetSelection();
        ExecutedActionsScript = actionsScript.actionList;
        ExecutedActionInt = actionsScript.actionInt;
        indexAction = -1;
        Action();
    }

    public void Action(Event action,ScriptableAction actionsScript)
    {
        ResetSelection();
        ExecutedActionsScript = actionsScript.actionList;
        ExecutedActionInt = actionsScript.actionInt;
        indexAction = 0;
        Action();
    }

    public void ResetSelection()
    {
        SelectedCard = null;
    }

    public void SelectOnHand(int x)
    {
        GameState = State.playerSelectHand;
        CardToSelectedNumber = x;
    }

    public void SelectOnGame(int x)
    {
        GameState = State.playerSelectGame;
        CardToSelectedNumber = x;
    }

    public void AddCard(Card cardToAdd)
    {
        foreach(Card c in SelectedCard)
        {
            if (cardToAdd == c) return;
        }
        SelectedCard.Add(cardToAdd);
        CheckForEndSelection();
    }

    private void CheckForEndSelection()
    {
        if(selectedCard.Count>= CardToSelectedNumber)
        {
            GameState = State.playerAttack;
            Action();
        }
    }

    #endregion
}
