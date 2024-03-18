using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Input_Controller : MonoBehaviour
{
    public TextMeshProUGUI turnText; // Text component to display current turn count
    public TextMeshProUGUI matchText; // Text component to display current match count

    private int currentTurn = 0; // Current turn count
    public int currentMatches = 0; // Current match count
    private Cards selectedCard1; // First selected card
    private Cards selectedCard2; // Second selected card
    private bool isTurnOver = true; // Flag to check if turn is over
    void Start()
    {
        UpdateTurnText();
        UpdateMatchText();
    }
   
    void Update()
    {
        // Check if the left mouse button is clicked and the turn is over
        if (Input.GetMouseButtonDown(0) && isTurnOver)
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.OnMouseClick);
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Get the clicked UI object
                GameObject clickedObject = GetClickedUIObject();

                // Check if the clicked object exists and has the Cards component
                if (clickedObject != null)
                {
                    Cards card = clickedObject.GetComponent<Cards>();
                    if (card != null && !card.isFaceUp)
                    {
                        SoundManager.Instance.PlayClip(SoundManager.Instance.ClickOnCard);

                        Debug.Log("Card Name: " + card.Card_Name);
                        SelectCard(card);
                    }
                }
                else
                {
                    Debug.Log("Clicked object is null.");
                }
            }
        }

    }
    GameObject GetClickedUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // Create a list to store the raycast results
        var raycastResults = new System.Collections.Generic.List<RaycastResult>();

        // Perform the raycast using all GraphicRaycasters
        EventSystem.current.RaycastAll(eventData, raycastResults);

        // Loop through the results to find the first valid hit
        foreach (var result in raycastResults)
        {
            // Ensure the raycast hit a valid UI object
            if (result.gameObject != null && result.gameObject.GetComponent<Cards>() != null)
            {
                return result.gameObject;
            }
        }

        return null;
    }
    void SelectCard(Cards card)
    {
        if (selectedCard1 == null)
        {
            selectedCard1 = card;
            selectedCard1.Flip();
        }
        else if (selectedCard2 == null && card != selectedCard1)
        {
            selectedCard2 = card;
            selectedCard2.Flip();
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isTurnOver = false;
        currentTurn++;

        yield return new WaitForSeconds(1f); // Wait for 1 second before checking match

        if (selectedCard1.Card_Name == selectedCard2.Card_Name)
        {
            currentMatches++;
            selectedCard1.gameObject.SetActive(false);
            selectedCard2.gameObject.SetActive(false);
            UpdateMatchText();
            SoundManager.Instance.PlayClip(SoundManager.Instance.CardMatch);


        }
        else
        {
            selectedCard1.Flip();
            selectedCard2.Flip();
            SoundManager.Instance.PlayClip(SoundManager.Instance.CardMisMatch);

        }

        selectedCard1 = null;
        selectedCard2 = null;
        isTurnOver = true;
        UpdateTurnText();
    }

    void UpdateTurnText()
    {
        turnText.text = currentTurn.ToString();
    }

    void UpdateMatchText()
    {
        matchText.text =currentMatches.ToString();
    }
}
