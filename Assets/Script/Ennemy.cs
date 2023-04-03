using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy
{
    int PV;
    int Attack;
    Sprite SpriteEnnemy;

    public Ennemy(ScriptableEnnemy SOennemy)
    {
        PV = SOennemy.PV;
        Attack = SOennemy.Attack;
        SpriteEnnemy = SOennemy.Image;
    }

    public int AttackMob()
    {
        return Random.Range(1, Attack);
    }

    public void TakeDamage(int dmg)
    {
        PV -= dmg;
        if (PV <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("je meurs");
        FightManager.Instance.FightFinish(this);
        //met l'ennemi pas selectionnable si plusieurs ennemis (uniquement si le temps)
    }
}
