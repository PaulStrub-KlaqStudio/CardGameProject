using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.VersionControl;
using Unity.VisualScripting;
using UnityEditor.U2D;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.GraphicsBuffer;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.PackageManager.UI;

public class TestTool : EditorWindow
{
    string CardName = "CardName";
    Sprite ImageCard;
    Texture2D ImageCardMob;
    Texture2D ImageCardEvent;
    Vector2 scrollPosition;

    ScriptableAction[] actionsSOList;
    ScriptableMob[] MobSOList;
    ScriptableEvent[] EventSOList;

    float windowWidth;
    float windowHeight;
    float DemiWidth;

    #region Mob

    bool IsMob = true;
    int PV = 0;
    int Stamina = 0;

    bool EditAction1 = false;
    bool NewAction1 = false;
    bool WasNewAction1 = true;
    ScriptableAction Action1Scriptable;

    bool EditAction2 = false;
    bool NewAction2 = false;
    bool WasNewAction2 = true;
    ScriptableAction Action2Scriptable;

    #endregion

    #region Event

    bool newAction = false;
    ScriptableAction ActionEvent;
    bool WasNewActionEvent = true;

    #endregion

    private void OnEnable()
    {
        ImageCardMob = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Assets/CardMob.png", typeof(Texture2D));
        ImageCardEvent = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Assets/CardEvent.png", typeof(Texture2D));
        Object[] SOAObjectList = Resources.LoadAll("Scriptable/Mob/ScriptableAction/");
        actionsSOList = new ScriptableAction[SOAObjectList.Length];
        LoadLists();
    } // au lancement de la cherche tous les scriptableAction deja existants

    [MenuItem("GameObject/CardCreator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TestTool)).Show();
        GetWindow(typeof(TestTool)).maxSize = new Vector2(215f, 110f);
        GetWindow(typeof(TestTool)).minSize = GetWindow(typeof(TestTool)).maxSize;

    } // lance la fenêtre

    private void OnGUI()
    {

        windowWidth = position.width;
        windowHeight = position.height;
        DemiWidth = windowWidth / 2;

        GUILayout.BeginHorizontal();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowWidth/2));

        GUILayout.BeginVertical();
        
        if (IsMob)
        {
            IsMob = EditorGUILayout.Toggle("IsAMob", IsMob);
        }
        else
        {
            IsMob = EditorGUILayout.Toggle("IsEvent", IsMob);
        }
        SetCardDefault();
        if (IsMob)
        {
            SetMob();
            
        }
        else
        {
            SetEvent();
            
        }

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        if(IsMob)
        {
            GUILayout.EndHorizontal();
            DiplayMobPreview();
        }
        else
        {
            GUILayout.EndHorizontal();
            DisplayEventPreview();
        }
        

        DisplayGenerationButton();
        
    }

    // Preview de la carte du mob
    void DiplayMobPreview()
    {

        if (ImageCard != null)
        {
            GUI.DrawTexture(new Rect((windowWidth / 2 + DemiWidth / 5), windowHeight / 24 * 2, DemiWidth / 20 * 12, (windowHeight / 12 * 2) + windowHeight / 8), ImageCard.texture);
        }
        GUI.DrawTexture(new Rect((windowWidth / 2 + DemiWidth / 5), windowHeight / 24 * 1, DemiWidth / 10 * 6, windowHeight / 12 * 10), ImageCardMob);

        Rect rectName = new Rect((windowWidth / 2 + DemiWidth / 3), windowHeight / 24 * 1, DemiWidth / 5, windowHeight / 24);
        GUIStyle styleName = new GUIStyle();
        styleName.fixedWidth = DemiWidth / 3;
        styleName.fixedHeight = (windowHeight / 24);
        styleName.alignment = TextAnchor.MiddleCenter;
        styleName.fontSize = (int)windowHeight / 48;
        EditorGUI.LabelField(rectName, CardName, styleName);

        Rect rectPV = new Rect((windowWidth / 2 + DemiWidth / 5), windowHeight / 24 * 9, DemiWidth / 5, windowHeight / 24);
        GUIStyle stylePV = new GUIStyle();
        stylePV.fixedWidth = DemiWidth / 3;
        stylePV.fixedHeight = (windowHeight / 24);
        stylePV.alignment = TextAnchor.LowerCenter;
        stylePV.fontSize = (int)windowHeight / 48;
        EditorGUI.LabelField(rectPV, PV.ToString(), stylePV);

        Rect rectStamina = new Rect((windowWidth / 2 + DemiWidth / 5 * 3), windowHeight / 24 * 9, DemiWidth / 5, windowHeight / 24);
        GUIStyle styleStamina = new GUIStyle();
        styleStamina.fixedWidth = DemiWidth / 6;
        styleStamina.fixedHeight = (windowHeight / 24);
        styleStamina.alignment = TextAnchor.LowerLeft;
        styleStamina.fontSize = (int)windowHeight / 48;
        EditorGUI.LabelField(rectStamina, Stamina.ToString(), styleStamina);


        styleStamina.fixedWidth = DemiWidth / 3;
        styleStamina.alignment = TextAnchor.LowerRight;
        EditorGUI.LabelField(rectPV, "Stamina", styleStamina);

        rectPV = new Rect((windowWidth / 2 + DemiWidth / 4), windowHeight / 24 * 9, DemiWidth / 4, windowHeight / 24);
        stylePV.fixedWidth = DemiWidth / 12;
        stylePV.alignment = TextAnchor.LowerLeft;
        EditorGUI.LabelField(rectPV, "PV", stylePV);


        if (Action1Scriptable != null)
        {
            Rect rectAction1 = new Rect((windowWidth / 2 + DemiWidth / 3), windowHeight / 24 * 9, DemiWidth / 20 * 12, (windowHeight / 12 * 2) + windowHeight / 6);
            GUIStyle styleAction1 = new GUIStyle();
            styleAction1.fixedWidth = DemiWidth / 3;
            styleAction1.fixedHeight = (windowHeight / 12 * 2) + windowHeight / 5;
            styleAction1.alignment = TextAnchor.MiddleCenter;
            styleAction1.fontSize = (int)windowHeight / 48;
            EditorGUI.LabelField(rectAction1, Action1Scriptable.Description, styleAction1);

            Rect rectExhaust = new Rect((windowWidth / 2 + DemiWidth / 5 * 3), windowHeight / 24 * 11, DemiWidth / 5, windowHeight / 24);
            GUIStyle styleExhaust = new GUIStyle();
            styleExhaust.fixedWidth = DemiWidth / 6;
            styleExhaust.fixedHeight = (windowHeight / 24);
            styleExhaust.alignment = TextAnchor.UpperCenter;
            styleExhaust.fontSize = (int)windowHeight / 48;
            EditorGUI.LabelField(rectExhaust, "Exhaust " + Action1Scriptable.exhaust.ToString(), styleExhaust);
        }
        if (Action2Scriptable != null)
        {
            Rect rectAction2 = new Rect((windowWidth / 2 + DemiWidth / 3), windowHeight / 24 * 14, DemiWidth / 20 * 12, (windowHeight / 12 * 2) + windowHeight / 6);
            GUIStyle styleAction2 = new GUIStyle();
            styleAction2.fixedWidth = DemiWidth / 3;
            styleAction2.fixedHeight = (windowHeight / 12 * 2) + windowHeight / 5;
            styleAction2.alignment = TextAnchor.MiddleCenter;
            styleAction2.fontSize = (int)windowHeight / 48;
            EditorGUI.LabelField(rectAction2, Action2Scriptable.Description, styleAction2);

            Rect rectExhaust = new Rect((windowWidth / 2 + DemiWidth / 5 * 3), windowHeight / 24 * 16, DemiWidth / 5, windowHeight / 24);
            GUIStyle styleExhaust = new GUIStyle();
            styleExhaust.fixedWidth = DemiWidth / 6;
            styleExhaust.fixedHeight = (windowHeight / 24);
            styleExhaust.alignment = TextAnchor.UpperCenter;
            styleExhaust.fontSize = (int)windowHeight / 48;
            EditorGUI.LabelField(rectExhaust, "Exhaust " + Action2Scriptable.exhaust.ToString(), styleExhaust);
        }
    }

    // Preview de la carte event
    void DisplayEventPreview()
    {
        if (ImageCard != null)
        {
            GUI.DrawTexture(new Rect((windowWidth / 2 + DemiWidth / 5), windowHeight / 24 * 2, DemiWidth / 20 * 12, (windowHeight / 12 * 2) + windowHeight / 6), ImageCard.texture);
        }
        GUI.DrawTexture(new Rect((windowWidth / 2 + DemiWidth / 5), windowHeight / 24 * 1, DemiWidth / 10 * 6, windowHeight / 12 * 10), ImageCardEvent);

        Rect rectName = new Rect((windowWidth / 2 + DemiWidth / 3), windowHeight / 24 * 1, DemiWidth / 5, windowHeight / 24);
        GUIStyle styleName = new GUIStyle();
        styleName.fixedWidth = DemiWidth / 3;
        styleName.fixedHeight = (windowHeight / 24);
        styleName.alignment = TextAnchor.MiddleCenter;
        styleName.fontSize = (int)windowHeight / 48;
        EditorGUI.LabelField(rectName, CardName, styleName);

        if (ActionEvent != null && ActionEvent.Description != null)
        {
            Rect rectEvent = new Rect((windowWidth / 2 + DemiWidth / 3 ), windowHeight / 24 * 11, DemiWidth / 20 * 12, (windowHeight / 12 * 2) + windowHeight / 6);
            GUIStyle styleEvent = new GUIStyle();
            styleEvent.fixedWidth = DemiWidth / 3;
            styleEvent.fixedHeight = (windowHeight / 12 * 2) + windowHeight / 5;
            styleEvent.alignment = TextAnchor.MiddleCenter;
            styleEvent.fontSize = (int)windowHeight / 48;
            EditorGUI.LabelField(rectEvent, ActionEvent.Description, styleEvent);
        }
    }

    // Fait Spawn le bouton
    void DisplayGenerationButton() 
    {
        Rect rectButton = new Rect((windowWidth / 2 + DemiWidth / 3), windowHeight / 24 * 22, DemiWidth / 3, (windowHeight / 12));
        GUIStyle styleButton = new GUIStyle();
        if (GUI.Button(rectButton, "Generate"))
        {
            GenerateCard();
            GUIUtility.ExitGUI();
        }
        GUIUtility.ExitGUI();
    }

    #region CardParametres

    // modifie les aspects de la carte similaire pour un event ou un mob
    void SetCardDefault()
    {
        CardName = EditorGUILayout.TextField("Card Nameeeee", CardName);
        ImageCard = (Sprite)EditorGUILayout.ObjectField(ImageCard, typeof(Sprite), false);

    } 

    #region Event

    // création de l'event
    void SetEvent()
    {
        newAction = EditorGUILayout.Toggle("new Action", newAction);
        if(newAction)
        {
            if (!WasNewActionEvent)
            {
                WasNewActionEvent = true;
                ActionEvent = null;
            }
            NewAction();
        }
        else
        {
            if (WasNewActionEvent)
            {
                WasNewActionEvent = false;
                ActionEvent = null;
            }
            SelectAction();
        }
    }

    // si nouvelle action permet de la modifier
    void NewAction()
    {
        if (ActionEvent == null)
        {
            ActionEvent = CreateInstance<ScriptableAction>();
        }

        SerializedObject serializedObjectAction1 = new SerializedObject(ActionEvent);
        EditorGUILayout.PropertyField(serializedObjectAction1.FindProperty("Name"));
        EditorGUILayout.PropertyField(serializedObjectAction1.FindProperty("Exhaust"));
        EditorGUILayout.PropertyField(serializedObjectAction1.FindProperty("Description"));
        serializedObjectAction1.ApplyModifiedProperties();

        if (GUILayout.Button("AddAction"))
        {
            ActionEvent.AddAction();
        }

        for (int i = 0; i < ActionEvent.ActionStructList.Count; i++)
        {
            ScriptableAction.ActionStruct ms = ActionEvent.ActionStructList[i];
            ms.Action = (ActionScript)EditorGUILayout.ObjectField(ms.Action, typeof(ActionScript), false);
            ms.ActionValue = EditorGUILayout.IntSlider("Value", ms.ActionValue, 0, 200);
            ActionEvent.ActionStructList[i] = ms;
            if (GUILayout.Button("RemoveAction"))
            {
                ActionEvent.RemoveAction(i);
            }
        }
    }

    // permet de selectionner une action deja existante
    void SelectAction()
    {
        ActionEvent = (ScriptableAction)EditorGUILayout.ObjectField(ActionEvent, typeof(ScriptableAction), false);
    }

    #endregion

    #region Mob

    // lance la modif des différents paramètre du mob
    void SetMob()
    {
        SetPV();
        SetStamina();
        DoModifiyAction1();
        DoModifiyAction2();
    }

    //assigne les pv
    void SetPV()
    {
        PV = EditorGUILayout.IntSlider("PV", PV, 0, 200);
    }

    //assigne la stamina
    void SetStamina()
    {
        Stamina = EditorGUILayout.IntSlider("Stamina", Stamina, 0, 200);
    }

    // souhaite on modifier l'action 1
    void DoModifiyAction1()
    {
        EditorGUILayout.BeginHorizontal();
        EditAction1 = EditorGUILayout.Toggle("EditAction1", EditAction1);
        if (EditAction1)
        {
            NewAction1 = EditorGUILayout.Toggle("NewAction1", NewAction1);
        }
        EditorGUILayout.EndHorizontal();
        if (EditAction1 && NewAction1)
        {
            if (!WasNewAction1)
            {
                WasNewAction1 = true;
                Action1Scriptable = null;
            }
            SetAction1();
        }
        else if (EditAction1 && !NewAction1)
        {
            if (WasNewAction1)
            {
                WasNewAction1 = false;
                Action1Scriptable = null;
            }
            SelectAction1();
        }
    } 

    void SelectAction1()
    {
        Action1Scriptable = (ScriptableAction)EditorGUILayout.ObjectField(Action1Scriptable, typeof(ScriptableAction), false);
    } // choisir une action déjà axistante

    // créer une nouvelle action1
    void SetAction1()
    {

        if(Action1Scriptable==null)
        {
            Action1Scriptable = CreateInstance<ScriptableAction>();
        }

        SerializedObject serializedObjectAction1 = new SerializedObject(Action1Scriptable);
        EditorGUILayout.PropertyField(serializedObjectAction1.FindProperty("Name"));
        EditorGUILayout.PropertyField(serializedObjectAction1.FindProperty("Exhaust"));
        EditorGUILayout.PropertyField(serializedObjectAction1.FindProperty("Description"));
        serializedObjectAction1.ApplyModifiedProperties();

        if (GUILayout.Button("AddAction"))
        {
            Action1Scriptable.AddAction();
        }

        for (int i = 0; i < Action1Scriptable.ActionStructList.Count; i++)
        {
            ScriptableAction.ActionStruct ms = Action1Scriptable.ActionStructList[i];
            ms.Action = (ActionScript)EditorGUILayout.ObjectField(ms.Action, typeof(ActionScript), false);
            ms.ActionValue = EditorGUILayout.IntSlider("Value", ms.ActionValue, 0, 200);
            Action1Scriptable.ActionStructList[i] = ms;
            if (GUILayout.Button("RemoveAction"))
            {
                Action1Scriptable.RemoveAction(i);
            }
        }

    }

    // souhaite on modifier l'action 2
    void DoModifiyAction2()
    {
        EditorGUILayout.BeginHorizontal();
        EditAction2 = EditorGUILayout.Toggle("EditAction2", EditAction2);
        if (EditAction2)
        {
            NewAction2 = EditorGUILayout.Toggle("NewAction2", NewAction2);
        }
        EditorGUILayout.EndHorizontal();
        if (EditAction2 && NewAction2)
        {
            if (!WasNewAction2)
            {
                WasNewAction2 = true;
                Action2Scriptable = null;
            }
            SetAction2();
        }
        else if (EditAction2 && !NewAction2)
        {
            if (WasNewAction2)
            {
                WasNewAction2 = false;
                Action2Scriptable = null;
            }
            SelectAction2();
        }
    }

    // choisir une action déjà existante 
    void SelectAction2()
    {
        Action2Scriptable = (ScriptableAction)EditorGUILayout.ObjectField(Action2Scriptable, typeof(ScriptableAction), false);
    }

    // créer une nouvelle action
    void SetAction2()
    {

        if (Action2Scriptable == null)
        {
            Action2Scriptable = CreateInstance<ScriptableAction>();
        }

        SerializedObject serializedObjectAction2 = new SerializedObject(Action2Scriptable);
        EditorGUILayout.PropertyField(serializedObjectAction2.FindProperty("Name"));
        EditorGUILayout.PropertyField(serializedObjectAction2.FindProperty("Exhaust"));
        EditorGUILayout.PropertyField(serializedObjectAction2.FindProperty("Description"));
        serializedObjectAction2.ApplyModifiedProperties();

        if (GUILayout.Button("AddAction"))
        {
            Action2Scriptable.AddAction();
        }
        for (int i = 0; i < Action2Scriptable.ActionStructList.Count; i++)
        {
            ScriptableAction.ActionStruct ms = Action2Scriptable.ActionStructList[i];
            ms.Action = (ActionScript)EditorGUILayout.ObjectField(ms.Action, typeof(ActionScript), false);
            ms.ActionValue = EditorGUILayout.IntSlider("Value", ms.ActionValue, 0, 200);
            Action2Scriptable.ActionStructList[i] = ms;
            if (GUILayout.Button("RemoveAction"))
            {
                Action2Scriptable.RemoveAction(i);
            }

        }

    }

    #endregion

    #endregion

    #region GenerationCard

    // regarde le type de carte à générer 
    void GenerateCard()
    {
        if(IsMob && CanGenerateMob())
        {
            GenerateMob();
        }
        else if(CanGenerateEvent())
        {
            GenerateEvent();
        }
    }

    // a t on tous les éléments pour créer une carte 
    bool CanGenerateMob()
    {
        if(CanCreateAction(Action1Scriptable) && CanCreateAction(Action2Scriptable) && ImageCard != null && CardName != null && !MobHasSameName())
        {
            if (NewAction1)
                if (ActionHasSameName(Action1Scriptable))
                    return false;
            if (NewAction2)
                if (ActionHasSameName(Action2Scriptable))
                    return false;
            return true;
        }
        else
            return false;
    }

    // a t on tous les éléments pour créer une action
    bool CanCreateAction(ScriptableAction actionCurrent)
    {
        if(actionCurrent != null)
        {
            if (actionCurrent.Name != null && actionCurrent.actionList != null && actionCurrent.Description != null )
            {
                return true;
            }
            else
            return false;
        }
        else 
            return false;
    }

    // lance la génération des actions et de la carte
    void GenerateMob()
    {
        if (NewAction1)
        {
            GenerateAction(Action1Scriptable);
        }
        if (NewAction2)
        {
            GenerateAction(Action2Scriptable);
        }
        GenerateFullCardMob();
    }

    // génère l'action et la remplace si une action faisant exactement la meme chose existe
    void GenerateAction(ScriptableAction actionCurrent)
    {

            ScriptableAction so = new ScriptableAction();
            if (!CanSimplifyCreation(actionCurrent, out so))
            {
                AssetDatabase.CreateAsset(actionCurrent, "Assets/Resources/Scriptable/Mob/ScriptableAction/" + actionCurrent.Name + ".asset");
                AssetDatabase.SaveAssets();
            }
            else
            {
                if (actionCurrent == Action1Scriptable)
                    Action1Scriptable = so;
                else if (actionCurrent == Action2Scriptable)
                    Action2Scriptable = so;
                else if (actionCurrent == ActionEvent)
                    ActionEvent = so;
            }
        
    } 

    // return true si l'action donnée à le même nom qu'une action déjà existante
    bool ActionHasSameName(ScriptableAction actionCurrent)
    {
        foreach(ScriptableAction s in actionsSOList)
        {
            if(s.Name == actionCurrent.Name)
            {
                actionCurrent.Name = null;
                return true;
            }
        }
        return false;
    }

    // return true si le mob donné à le même nom qu'un mob déjà existant
    bool MobHasSameName()
    {
        foreach (ScriptableMob s in MobSOList)
        {
            if (s.Name == CardName)
            {
                CardName = null;
                return true;
            }
        }
        return false;
    }

    // return true si l'event donné à le même nom qu'un event déjà existant
    bool EventHasSameName()
    {
        foreach (ScriptableEvent s in EventSOList)
        {
            if (s.Name == CardName)
            {
                CardName=null;
                return true;
            }
        }
        return false;
    }

    // dans le cas de la création d'une nouvelle action, vérifie si une action similaire existe deja et la remplace
    bool CanSimplifyCreation(ScriptableAction actionCurrent, out ScriptableAction so)
    {
        Debug.Log(actionCurrent.Name);
        foreach(ScriptableAction s in actionsSOList)
        {
            if (IsSameStruct(s.ActionStructList,actionCurrent.ActionStructList) && s.exhaust==actionCurrent.exhaust)
            {
                so = s;
                return true;
            }
        }
        so = null;
        return false;
    } 

    // return true si deux list de ScriptableAction.ActionStruct sont égales
    public bool IsSameStruct(List<ScriptableAction.ActionStruct> s, List<ScriptableAction.ActionStruct> ac)
    {
        if (ac.Count == s.Count)
        {

            for (int i = 0; i < s.Count; i++)
            {
                    Debug.Log("je passe dans le truc");
                    if (s[i].Action != ac[i].Action && s[i].ActionValue != ac[i].ActionValue)
                    {
                        Debug.Log("Value not same");
                        return false;
                    } 
            
            }
            return true;
        }
        return false;
    }

    // génère la carte Mob
    void GenerateFullCardMob()
    {
        ScriptableMob mobToCreate = CreateInstance<ScriptableMob>();

        mobToCreate.Name = CardName;
        mobToCreate.MobImage = ImageCard;
        mobToCreate.stamina = Stamina;
        mobToCreate.pv = PV;
        mobToCreate.action1 = Action1Scriptable;
        mobToCreate.action2 = Action2Scriptable;
        AssetDatabase.CreateAsset(mobToCreate, "Assets/Resources/Scriptable/Mob/Mob/" + CardName + ".asset");
        AssetDatabase.SaveAssets();
        LoadLists();
        Reset();
    }

    // génère la carte Event
    void GenerateFullCardEvent()
    {
        ScriptableEvent eventToCreate = CreateInstance<ScriptableEvent>();

        eventToCreate.Name = CardName;
        eventToCreate.EventImage = ImageCard;
        eventToCreate.Action = ActionEvent;
        AssetDatabase.CreateAsset(eventToCreate, "Assets/Resources/Scriptable/Mob/Event/" + CardName + ".asset");
        AssetDatabase.SaveAssets();
        LoadLists();
        Reset();
    } 

    //après qu'une carte ait été générée elle reset ses informations
    private void Reset()
    {
        Action1Scriptable = null;
        Action2Scriptable = null;
        ActionEvent = null;
        CardName = null;
        ImageCard = null;
        PV = 0;
        Stamina = 0;
    }

    // après création d'une carte recharge la liste de scriptableAction, d'Event et de mob
    void LoadLists()
    {
        LoadActionList();
        LoadMobList();
        LoadEventList();
    }

    void LoadActionList()
    {
        Object[] SOAObjectList = Resources.LoadAll("Scriptable/Mob/ScriptableAction/");
        actionsSOList = new ScriptableAction[SOAObjectList.Length];
        for (int i = 0; i < SOAObjectList.Length; i++)
        {
            Debug.Log("je genere les actions");
            if (SOAObjectList[i].GetType().Equals(typeof(ScriptableAction)))
            {
                actionsSOList[i] = (ScriptableAction)SOAObjectList[i];
            }
        }
    }

    void LoadMobList()
    {
        Object[] SOMobList = Resources.LoadAll("Scriptable/Mob/Mob/");
        MobSOList = new ScriptableMob[SOMobList.Length];
        for (int i = 0; i < SOMobList.Length; i++)
        {
            if (SOMobList[i].GetType().Equals(typeof(ScriptableMob)))
            {
                MobSOList[i] = (ScriptableMob)SOMobList[i];
            }
        }
    }

    void LoadEventList()
    {
        Object[] SOAEventList = Resources.LoadAll("Scriptable/Mob/Event/");
        EventSOList = new ScriptableEvent[SOAEventList.Length];
        for (int i = 0; i < SOAEventList.Length; i++)
        {
            if (SOAEventList[i].GetType().Equals(typeof(ScriptableEvent)))
            {
                EventSOList[i] = (ScriptableEvent)SOAEventList[i];
            }
        }
    }

    // a t on tous les éléments pour créer une carte 
    bool CanGenerateEvent()
    {
        if (CanCreateAction(ActionEvent) && ImageCard != null && CardName != null && !EventHasSameName())
        {
            if (newAction)
                if (ActionHasSameName(ActionEvent))
                    return false;
            return true;
        }
        else
            return false;
    }

    // lance la génération des actions et de la carte
    void GenerateEvent()
    {
        if(newAction)
            GenerateAction(ActionEvent);
        GenerateFullCardEvent();
    } 

    #endregion
}
