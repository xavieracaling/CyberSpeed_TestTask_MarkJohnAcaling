using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public List<Card> Cards { get; private set; }

    public void ResetDeck()
    {
        foreach (var card in Cards)
        {
            card.ResetCard();
        }
    }
}
