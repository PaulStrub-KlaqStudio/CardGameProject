using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [Header("Canva")]
    [SerializeField] private GameObject MainCanva;
    [SerializeField] private GameObject RulesCanva;

    // Start is called before the first frame update
    void Start()
    {
        RulesCanva.SetActive(false);
        MainCanva.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoRules()
    {
        RulesCanva.SetActive(true);
        MainCanva.SetActive(false);
    }

    public void BackRules()
    {
        RulesCanva.SetActive(false);
        MainCanva.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
