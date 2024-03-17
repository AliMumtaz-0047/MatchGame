using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public List<GameObject> cards; // List of existing card objects
    public List<string> cardNames; // List of card names
    public List<Sprite> frontSprites; // List of front sprites
    public Sprite backSprite; // Back sprite
    public float showTime = 5f; // Time to show the cards before returning them to the original state

    public void Awake()
    {
        Initialize_Game();
    }
    public void Initialize_Game()
    {
        if (cards == null || cards.Count == 0)
        {
            Debug.LogError("No cards assigned to GameController!");
            return;
        }

        if (cardNames == null || cardNames.Count == 0)
        {
            Debug.LogError("No card names assigned to GameController!");
            return;
        }

        if (frontSprites == null || frontSprites.Count == 0)
        {
            Debug.LogError("No front sprites assigned to GameController!");
            return;
        }

        if (cardNames.Count != frontSprites.Count)
        {
            Debug.LogError("Number of card names and front sprites must match!");
            return;
        }
        int numPairs = frontSprites.Count / 2;
        // Shuffle front sprites
        Shuffle(frontSprites);
        Shuffle(cards);
        // Assign properties for each pair of cards
        for (int i = 0; i < numPairs && i < frontSprites.Count; i++)
        {
            // Get the current front sprite
            Sprite frontSprite = frontSprites[i];

            // Find the two cards with the same front sprite
            int cardIndex1 = i * 2;
            int cardIndex2 = i * 2 + 1;

            if (cardIndex1 < cards.Count && cardIndex2 < cards.Count)
            {
                // Assign properties to the cards
                cards[cardIndex1].GetComponent<Cards>().SetCardProperties("Card " + (i + 1), backSprite, frontSprite);
                cards[cardIndex2].GetComponent<Cards>().SetCardProperties("Card " + (i + 1), backSprite, frontSprite);
            }
            else
            {
                //Debug.LogError("Not enough cards to pair with front sprite: " + frontSprite.name);
                break;
            }
        }
        StartCoroutine(ShowCardsForSeconds());
    }
    IEnumerator ShowCardsForSeconds()
    {
        // Flip all the cards to show their front side
        FlipAllCards();

        // Wait for the specified show time
        yield return new WaitForSeconds(showTime);

        // Flip all the cards back to show their back side
        FlipAllCards();
    }
    void FlipAllCards()
    {
        for (int i=0;i<cards.Count;i++)
        {
            
            
                cards[i].GetComponent<Cards>().Flip();
            Debug.Log(cards[i].GetComponent<Cards>().name);
            
        }
    }
    // Fisher-Yates shuffle algorithm
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
