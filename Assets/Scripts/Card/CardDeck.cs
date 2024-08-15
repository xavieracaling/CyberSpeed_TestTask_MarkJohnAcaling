using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDeck : MonoBehaviour
{
    public List<Card> Cards = new List<Card>();

    private void Start() {
        
        Initialize();
    }
    public void Initialize()
    {
        GameManager.Instance.CurrentCarDeck = this;
        cardsInitialize();
        ShuffleDeck();

    }
    void cardsInitialize()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Card card = Cards[i];
            card.Initialize(card.transform.GetChild(0).GetComponent<Image>().sprite,
                   card.transform.GetComponent<Image>().sprite);
        }
    }
    public void ShuffleDeck()
    {
        if(GameManager.Instance.SaveCard.Cards.Count == 0)
        {
            GameManager.Instance.InitializeInfo();
            GameManager.Instance.SaveCard = new CardSave();
            List<(GameObject, int)> cardsNewPosition = new List<(GameObject, int)>();
            for (int i = 0; i < Cards.Count; i++)
            {
                int position = Random.Range(0,Cards.Count);
                cardsNewPosition.Add((Cards[i].gameObject, position)) ;
                CardProcessSaving.CardInfoSave(new CardInfo(){
                        Position = position,
                        IsFlipped = false,
                        IsMatched = false,
                        IndexOfCardDeck = i
                    });
            }
            CardProcessSaving.SaveGameInfo();

            foreach ((GameObject, int) item in cardsNewPosition)
                item.Item1.transform.SetSiblingIndex(item.Item2);
        }
        else 
        {
            CardSave saveCard = GameManager.Instance.SaveCard;
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].transform.SetSiblingIndex(saveCard.Cards[i].Position);
                Cards[i].IsMatched = saveCard.Cards[i].IsMatched;
                Cards[i].IsFlipped = saveCard.Cards[i].IsFlipped;
                 
                if(Cards[i].IsFlipped )
                    Cards[i].FlipCard();
                else 
                    Cards[i].FlipBack();

            }
            if(saveCard.FlippedCards.Count > 0)
            {
                for (int i = 0; i < saveCard.FlippedCards.Count; i++)
                {
                    CardInfo cardInfo = saveCard.FlippedCards[i];
                    Card card = Cards[cardInfo.IndexOfCardDeck];
                    GameManager.Instance.FlippedCards.Add(card);
                }
            }
        }
        
        
    }
}
