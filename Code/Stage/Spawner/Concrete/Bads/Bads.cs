using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bads : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private List<Sprite> sprites;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    public void Init()
    {
        if(random == null)
        {
            random = new System.Random();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetActive()
    {
        sprite = sprites[random.Next(sprites.Count)];
        spriteRenderer.sprite = sprite;
    }

    private void OnMouseDown()
    {
        Debug.Log("I was clicked");
    }
}
