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
        List<(GameObject, int)> cardsNewPosition = new List<(GameObject, int)>();
        for (int i = 0; i < Cards.Count; i++)
            cardsNewPosition.Add((Cards[i].gameObject, Random.Range(0,Cards.Count))) ;

        foreach ((GameObject, int) item in cardsNewPosition)
            item.Item1.transform.SetSiblingIndex(item.Item2);
    }
}
