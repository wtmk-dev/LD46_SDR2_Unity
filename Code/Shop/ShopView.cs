using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Febucci.UI;
using DG.Tweening;

public class ShopView : MonoBehaviour
{
    [SerializeField]
    private Button powerBtn, damageBtn, speedBtn, chargeBtn,okBtn,noBtn;
    private Dictionary<string,Button> buttons;
    [SerializeField]
    private Image menu;
    [SerializeField]
    private TextAnimatorPlayer powerTxt, damageTxt, speedTxt, chargeTxt, shopTxt, exitShopTxt;
    private SpellModel spellModel;
    private Player playerModel;

    public void Init()
    {
        //this.spellModel = spellModel;
        //this.playerModel = playerModel;

        buttons = new Dictionary<string, Button>();

        buttons.Add("ok", okBtn);
        buttons.Add("no", noBtn);

        buttons.Add("p", powerBtn);
        buttons.Add("d", damageBtn);
        buttons.Add("c", chargeBtn);
        buttons.Add("s", speedBtn);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        powerBtn.image.DOFade(1f, .3f);
        powerBtn.interactable = false;
        damageBtn.image.DOFade(1f, .3f);
        damageBtn.interactable = false;
        speedBtn.image.DOFade(1f, .3f);
        speedBtn.interactable = false;
        chargeBtn.image.DOFade(1f, .3f);
        chargeBtn.interactable = false;

        menu.DOFade(1f, .3f);

        shopTxt.ShowText("Oh! Whad-up <shake>Player...</shake> <swing>its _BK</swing> here and ill be your shop..");

        okBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);
    }

    private void Update()
    {
        
    }

    public Button GetButton(string key)
    {
        return buttons[key];
    }


    public void ShowText(string txt)
    {
        shopTxt.ShowText(txt);
    }

    public void PowerUpdate(string txt)
    {
        powerTxt.ShowText(txt);
    }

    public void SpeedUpdate(string txt)
    {
        speedTxt.ShowText(txt);
    }

    public void ChargeUpdate(string txt)
    {
        chargeTxt.ShowText(txt);
    }

    public void DamageUpdate(string txt)
    {
        damageTxt.ShowText(txt);
    }

    public void ExitTextUpdate(string txt)
    {
        exitShopTxt.ShowText(txt);
    }

    public void StartShopping()
    {
        okBtn.gameObject.SetActive(false);

        powerBtn.interactable = true;
        damageBtn.interactable = true;
        speedBtn.interactable = true;
        chargeBtn.interactable = true;
    }
}
