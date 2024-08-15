using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Level;
    public const int MaxLevel = 4;
    public List<GameObject> ListOfLevelCardDecks = new List<GameObject>();
    public static GameManager Instance { get; private set; }

    public CardDeck CardDeck { get; private set; }

    public List<Card> FlippedCards = new List<Card>();
    public int matches;
    public int Matches { get => matches; private set { matches = value; UIManager.Instance.UpdateMatches(value);}  }
    public int turns;
    public int Turns { get => turns; private set { turns = value; UIManager.Instance.UpdateTurns(value);}  }
    

    public CardDeck CurrentCarDeck;

    public GameObject ContainerRestartNext;
    public GameObject NextLevelGO;
    public GameObject ContainerUI;
    public GameObject MenuUI;
    public Button RestartBTN;
    public Transform GameContainer;
    public CardSave SaveCard ;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        InitializeInfo();
        LoadCheckGame();
      
    }
    public void InitializeInfo()
    {
        FlippedCards.Clear();
        Matches = 0;
        Turns = 0;
    }

    public void OnCardSelected(Card selectedCard)
    {
        FlippedCards.Add(selectedCard);
        CardProcessSaving.AddFlipCard(selectedCard.CardIndex);
        if (FlippedCards.Count % 2 == 0)
            StartCoroutine(checkMatch());
        CardProcessSaving.SaveGameInfo();

    }

    private IEnumerator checkMatch()
    {
        // Get the last two flipped cards
        Turns++;
        int lastIndex = FlippedCards.Count - 1;
        Card card1 = FlippedCards[lastIndex];
        Card card2 = FlippedCards[lastIndex - 1];
        
        CardInfo cardInfo1 = CardProcessSaving.GetCardInfo(lastIndex);
        CardInfo cardInfo2 = CardProcessSaving.GetCardInfo(lastIndex - 1);
        yield return new WaitForSeconds(0.5f);

        if (card1.FrontSprite == card2.FrontSprite)
        {
            card1.SetMatched();
            card2.SetMatched();
            card1.MatchedSFX.Play();
            CardProcessSaving.SaveCardIsMatch(card1.CardIndex,true);
            CardProcessSaving.SaveCardIsMatch(card2.CardIndex,true);
            Matches++;
            CardProcessSaving.SaveGameInfo();

           
        }
        else
        {
            card1.FlipBack();
            card2.FlipBack();
            CardProcessSaving.SaveCardIsMatch(card1.CardIndex,false);
            CardProcessSaving.SaveCardIsMatch(card2.CardIndex,false);
        }

        FlippedCards.Remove(card1);
        FlippedCards.Remove(card2);
        CardProcessSaving.RemoveFlipCard(cardInfo1);
        CardProcessSaving.RemoveFlipCard(cardInfo2);
        CheckLevelComplete();

    }
    public void CheckLevelComplete()
    {
        if(matches + matches == CurrentCarDeck.Cards.Count)
        {
            AudioManager.Instance.GameWin.Play();
            PlayerPrefs.DeleteAll();
            GameManager.Instance.SaveCard = new CardSave();
            CurrentCarDeck.gameObject.SetActive(false);
            ContainerRestartNext.SetActive(true);
            NextLevelGO.SetActive(true);
            RestartBTN?.onClick?.RemoveAllListeners();
            int level = Level;
            RestartBTN.onClick.AddListener( () => NextLevel(level));
            if(MaxLevel == Level )
            {
                NextLevelGO.SetActive(false);
            }
            else
            {
                Button buttonNext = NextLevelGO.GetComponent<Button>();
                buttonNext?.onClick?.RemoveAllListeners();
                Level++;
                buttonNext.onClick.AddListener( () => NextLevel(Level));
            }
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
        ContainerRestartNext.SetActive(false);
        if(CurrentCarDeck != null)
            Destroy(CurrentCarDeck.gameObject);
        MenuUI.gameObject.SetActive(false);
        Level = level;
        GameObject cardLevel = Instantiate(ListOfLevelCardDecks[level], GameContainer);
        CardDeck cardDeck = cardLevel.GetComponent<CardDeck>();
        ContainerUI.SetActive(true);
    }

    public void LoadCheckGame()
    {
        if(CardProcessSaving.CheckSavedGame())
        {
            string savedCheck = PlayerPrefs.GetString("LevelSaved","null");
            Debug.Log(savedCheck);
            SaveCard = JsonConvert.DeserializeObject<CardSave>(savedCheck);
            Level = SaveCard.CurrentLevel;

            NextLevel(Level);
            Turns = SaveCard.Turns;
            Matches = SaveCard.Matches;

        }
    }
    
    
}
