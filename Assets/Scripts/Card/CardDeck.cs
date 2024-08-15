using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public List<Card> Cards = new List<Card>();

    private void Start() {
        Initialize();
    }
    public void Initialize()
    {
        GameManager.Instance.CurrentCarDeck = this;
        ShuffleDeck();
    }
    public void ShuffleDeck()
    {
        CardProcessSaving.SaveGameInfo();
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
        CardProcessSaving.SavedJsonGame();
        foreach ((GameObject, int) item in cardsNewPosition)
            item.Item1.transform.SetSiblingIndex(item.Item2);
        
    }
}
