using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Level;
    public const int MaxLevel = 5;
    public List<GameObject> ListOfLevelCardDecks = new List<GameObject>();
    public static GameManager Instance { get; private set; }

    public CardDeck CardDeck { get; private set; }

    private List<Card> flippedCards = new List<Card>();
    public int matches;
    public int Matches { get => matches; private set { matches = value; UIManager.Instance.UpdateMatches(value);}  }
    public int turns;
    public int Turns { get => turns; private set { turns = value; UIManager.Instance.UpdateTurns(value);}  }
    

    public CardDeck CurrentCarDeck;

    public GameObject ContainerRestartNext;
    public GameObject NextLevelGO;
    public GameObject ContainerUI;
    public GameObject MenuUI;
    public Transform GameContainer;
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
        
      
    }

    public void OnCardSelected(Card selectedCard)
    {
        flippedCards.Add(selectedCard);

        // Check if we have a pair to evaluate
        if (flippedCards.Count % 2 == 0)
        {
            StartCoroutine(checkMatch());
        }
    }

    private IEnumerator checkMatch()
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

        CheckLevelComplete();
    }
    public void CheckLevelComplete()
    {
        if(matches + matches == CurrentCarDeck.Cards.Count)
        {
            ContainerRestartNext.SetActive(true);
            NextLevelGO.SetActive(true);
            if(MaxLevel == Level )
                NextLevelGO.SetActive(false);
            Debug.Log("Completed Game!");
        }
    }
    public void GotoMenu()
    {
        MenuUI.gameObject.SetActive(true);
        ContainerUI.SetActive(false);
        CurrentCarDeck.gameObject.SetActive(false);
    }
    public void NextLevel(int level)
    {
        MenuUI.gameObject.SetActive(false);
        Level = level;
        InitializeGame();
        GameObject cardLevel = Instantiate(ListOfLevelCardDecks[level], GameContainer);
        CardDeck cardDeck = cardLevel.GetComponent<CardDeck>();
        ContainerUI.SetActive(true);
        cardDeck.ShuffleDeck();
    }
    public void Restart() => NextLevel(Level);
    
}
