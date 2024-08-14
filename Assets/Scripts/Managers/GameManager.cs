using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CardDeck CardDeck { get; private set; }

    private List<Card> flippedCards = new List<Card>();
    public int matches;
    public int Matches { get => matches; private set { matches = value; UIManager.Instance.UpdateMatches(value);}  }
    public int turns;
    public int Turns { get => turns; private set { turns = value; UIManager.Instance.UpdateTurns(value);}  }
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
        Matches = 0;
        Turns = 0;
        matchesFound = 0;
      
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
        Turns++;
        int lastIndex = flippedCards.Count - 1;
        Card card1 = flippedCards[lastIndex];
        Card card2 = flippedCards[lastIndex - 1];

        yield return new WaitForSeconds(0.5f);

        if (card1.FrontSprite == card2.FrontSprite)
        {
            card1.SetMatched();
            card2.SetMatched();
            Matches++;
            matchesFound += 2;

            // if (matchesFound >= CardDeck.Cards.Count)
            // {
            
            // }
        }
        else
        {
            card1.FlipBack();
            card2.FlipBack();
        }

        flippedCards.Remove(card1);
        flippedCards.Remove(card2);
    }
}
