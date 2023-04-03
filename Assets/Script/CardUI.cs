using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private GameObject Action1;
    [SerializeField] private GameObject Action2;
    [SerializeField] private GameObject ActionEvent;
    [SerializeField] private TextMeshProUGUI Action1Text;
    [SerializeField] private TextMeshProUGUI Action2Text;
    [SerializeField] private TextMeshProUGUI ActionEventText;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private Image Image;

    public void InitCardMob(Sprite imageChange,string nameChange, string action1Description, string action2Description)
    {
        ActionEvent.SetActive(false);
        Action1.SetActive(true);
        Action2.SetActive(true);
        Action1Text.text = action1Description;
        Action2Text.text = action2Description;
        Name.text = nameChange;
        Image.sprite = imageChange;
    }

    public void InitCardEvent(Sprite imageChange, string nameChange, string actionDescription)
    {
        ActionEvent.SetActive(true);
        Action1.SetActive(false);
        Action2.SetActive(false);
        ActionEventText.text = actionDescription;
        Name.text = nameChange;
        Image.sprite = imageChange;
    }

    public void InitCardMob(Mob cardMob)
    {
        ScriptableMob scriptableMob = cardMob.MobScriptable;
        ActionEvent.SetActive(false);
        Action2.SetActive(true);
        Action1.SetActive(true);
        Name.text = scriptableMob.Name;
        Image.sprite = scriptableMob.MobImage;
        Action1Text.text = scriptableMob.action1.Description;
        Action2Text.text = scriptableMob.action2.Description;
    }

    public void InitCardMobBoard(Mob cardMob)
    {
        ScriptableMob scriptableMob = cardMob.MobScriptable;
        Action2.SetActive(true);
        Action1.SetActive(true);
        Name.text = scriptableMob.Name;
        Image.sprite = scriptableMob.MobImage;
        Action1Text.text = scriptableMob.action1.Description;
        Action2Text.text = scriptableMob.action2.Description;
        ActionClick action1Script = Action1.GetComponent<ActionClick>();
        action1Script.MobReference = cardMob;
        ActionClick action2Script = Action2.GetComponent<ActionClick>();
        action2Script.MobReference = cardMob;
    }

    public void InitCardEvent(Event cardEvent)
    {
        ScriptableEvent scriptableEvent = cardEvent.EventScriptable;
        ActionEvent.SetActive(true);
        Action2.SetActive(false);
        Action1.SetActive(false);
        Name.text = scriptableEvent.Name;
        Image.sprite = scriptableEvent.EventImage;
        ActionEventText.text = scriptableEvent.Action.Description;
        
    }
}
