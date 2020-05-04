using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Febucci.UI;

public class StartView : MonoBehaviour
{
    [SerializeField]
    private Image bgImg, fgImg;
    [SerializeField]
    private Button chanceBlockBtn, newGameBtn, endlessBtn;
    [SerializeField]
    private GameObject title, subTitle;
    private TextAnimatorPlayer titleAnim, subTitleAnim;
    [SerializeField]
    private List<Button> buttons;
    private Dictionary<string, Button> buttonMap;
    private Dictionary<string, GameObject> gameObjectMap;

    public void Init()
    {
        titleAnim = title.GetComponent<TextAnimatorPlayer>();

        buttonMap = new Dictionary<string, Button>();
        gameObjectMap = new Dictionary<string, GameObject>();

        buttonMap.Add("story", buttons[0]);
        buttonMap.Add("pew", buttons[1]);
        buttonMap.Add("choice", buttons[2]);

        gameObjectMap.Add("bg", bgImg.gameObject);
        gameObjectMap.Add("fg", fgImg.gameObject);
    }

    public void SetText(string key, string text)
    {
        Debug.Log("set text: " + key);
        switch (key.ToLower())
        {
            case "title":
                titleAnim.ShowText(text);
                break;
        }
    }

    public void SetActive(string key, bool isActive)
    {
       // Debug.Log("set active " + key);
        switch (key.ToLower())
        {
            case "story":
                newGameBtn.gameObject.SetActive(isActive);
                break;
            case "pew":
                endlessBtn.gameObject.SetActive(isActive);
                break;
            case "title":
                title.SetActive(isActive);
                break;
            case "st":
                //Debug.Log("hide title");
               subTitle.SetActive(isActive);
                break;
            case "self":
                gameObject.SetActive(false);
                break;
        }
    }

    public Button GetButton(string key)
    {
        //Debug.Log("get button: " + key);
        return buttonMap[key];
    }

    public GameObject GetGameObject(string key)
    {
        //Debug.Log("get button: " + key);
        return gameObjectMap[key];
    }

    public void FadeObject(string key, float time)
    {
        //Debug.Log(gameObjectMap.Count);
        //Debug.Log("fade object: " + key);
        Image img = gameObjectMap[key].GetComponent<Image>();

        img.DOFade(0, time);
    }
}
