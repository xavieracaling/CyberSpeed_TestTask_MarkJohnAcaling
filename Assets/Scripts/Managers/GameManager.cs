using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CardDeck CardDeck { get; private set; }
    public UIManager UIManager { get; private set; }

    private List<Card> flippedCards = new List<Card>();
    private int score = 0;
    private int matchesFound = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        score = 0;
        matchesFound = 0;
        UIManager.Instance.UpdateScore(score);
        CardDeck.ResetDeck();
    }

    public void OnCardSelected(Card selectedCard)
    {
        flippedCards.Add(selectedCard);

        // Check if we have a pair to evaluate
        if (flippedCards.Count % 2 == 0)
        {
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        // Get the last two flipped cards
        int lastIndex = flippedCards.Count - 1;
        Card card1 = flippedCards[lastIndex];
        Card card2 = flippedCards[lastIndex - 1];

        yield return new WaitForSeconds(0.8f);

        if (card1.FrontSprite == card2.FrontSprite)
        {
            card1.SetMatched();
            card2.SetMatched();
            score++;
            matchesFound += 2;
            UIManager.Instance.UpdateScore(score);

            if (matchesFound >= CardDeck.Cards.Count)
            {
                
            }
        }
        else
        {
            card1.FlipBack();
            card2.FlipBack();
        }

        flippedCards.RemoveAt(lastIndex);
        flippedCards.RemoveAt(lastIndex - 1);
    }
}
