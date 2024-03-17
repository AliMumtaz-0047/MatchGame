using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public string Card_Name;
    public Sprite Back_Side;
    public Sprite Front_Side;
    private SpriteRenderer spriteRenderer;
    public bool isFaceUp = false;








    public void SetCardProperties(string cardName, Sprite backSprite, Sprite frontSprite)
    {
        Card_Name = cardName;
        Back_Side = backSprite;
        Front_Side = frontSprite;
        // Ensure spriteRenderer is initialized
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        // Set the back sprite initially
        SetBackSprite();
    }
    // Flip the card
    public void Flip()
    {
        isFaceUp = !isFaceUp;
        if (isFaceUp)
        {
            SetFrontSprite();
        }
        else
        {
            SetBackSprite();
        }
    }

    // Set the front sprite
    private void SetFrontSprite()
    {
        if (Front_Side != null)
        {
            spriteRenderer.sprite = Front_Side;
            this.GetComponent<Image>().sprite = Front_Side;
        }
        else
        {
            Debug.LogError("Front sprite is not set!");
        }
    }

    // Set the back sprite
    private void SetBackSprite()
    {
        if (Back_Side != null)
        {
            spriteRenderer.sprite = Back_Side;
            this.GetComponent<Image>().sprite = Back_Side;

        }
        else
        {
            Debug.LogError("Back sprite is not set!");
        }
    }
}
