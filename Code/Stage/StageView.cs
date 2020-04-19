using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Febucci.UI;

public class StageView : MonoBehaviour
{
    [SerializeField]
    private GameObject hopeMeter, hopeText, spellSlot, hud, narrator, scoreText;
    private Image hopeMeterImage, spellSlotImage, hudImage;
    [SerializeField]
    private TextAnimatorPlayer hopeTextAnim, narratorTextAnim, scoreTextAnim;

    private int currentHope;

    public void Init()
    {
        hopeMeterImage = hopeMeter.GetComponent<Image>();
        spellSlotImage = spellSlot.GetComponent<Image>();
        hudImage = hud.GetComponent<Image>();

        currentHope = 0;
        hopeMeter.SetActive(false);
        hopeText.SetActive(false);
        spellSlot.SetActive(false);
        hud.SetActive(false);
        narrator.SetActive(false);
    }

    public void SetHopeMeterActive()
    {
        hopeMeter.SetActive(true);
        hopeText.SetActive(true);
        hopeMeterImage.DOFade(1, 3f);
    }

    public void SetSpellSlotActive()
    {
        spellSlot.SetActive(true);
        spellSlotImage.DOFade(1, 5f);
    }

    public void SetHudActive()
    {
        hud.SetActive(true);
        hudImage.DOFade(1, 5f);
    }

    public void SetScoreText(string score)
    {
        scoreTextAnim.ShowText(score);
    }

    public void SetNarratorText(string text)
    {
        narrator.SetActive(true);
        narratorTextAnim.ShowText("<wiggle>" + text);
    }

    public void UpdateHopeMeter(float amount)
    {
        UpdateImageFill(hopeMeterImage, amount);
        UpdateHopeText(amount);
    }

    private void UpdateHopeText(float amount)
    {
        int ch = (int)amount;

        if(currentHope != ch)
        {
            currentHope = ch;
            string hope = ch + "";
            hopeTextAnim.ShowText(hope);
        }
    }

    private void UpdateImageFill(Image img, float amount)
    {
        img.fillAmount = amount / 100;
    }
}
