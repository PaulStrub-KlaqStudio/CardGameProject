using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void LoseGame()
    {
        Debug.Log("combat lose");
    }

    public void EndFight()
    {
        Debug.Log("combat win");
    }
}
