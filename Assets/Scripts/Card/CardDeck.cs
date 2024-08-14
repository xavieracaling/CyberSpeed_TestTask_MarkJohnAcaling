using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public static CardDeck Instance;
    public List<Card> Cards = new List<Card>();

    private void Awake() {
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
    public void ResetDeck()
    {
        if(Cards.Count < 1) return;
        foreach (var card in Cards)
        {
            card.ResetCard();
        }
    }
}
