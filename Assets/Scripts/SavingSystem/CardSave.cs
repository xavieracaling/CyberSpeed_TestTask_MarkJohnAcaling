using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
[Serializable]
public class CardSave
{
    public int CurrentLevel;
    public int Matches;
    public int Turns;
    public List<CardInfo> Cards = new List<CardInfo>();
    public List<CardInfo> FlippedCards = new List<CardInfo>();
        
}
[Serializable]
public class CardInfo
{
    public int Position;
    public int IndexOfCardDeck;
    public bool IsFlipped;
    public bool IsMatched;
}
public static class CardProcessSaving
{
    public static bool CheckSavedGame()
    {
        string savedCheck = PlayerPrefs.GetString("LevelSaved","null");
        if(savedCheck != "null")
            return true;
        return false;
    }
    public static void SavedJsonGame() 
    {
        PlayerPrefs.SetString("LevelSaved", JsonConvert.SerializeObject(GameManager.Instance.SaveCard) );
        PlayerPrefs.Save();
    }
    public static void SaveGameInfo()
    {
        GameManager.Instance.SaveCard.CurrentLevel = GameManager.Instance.Level;
        GameManager.Instance.SaveCard.Matches = GameManager.Instance.Matches;
        GameManager.Instance.SaveCard.Turns = GameManager.Instance.Turns;
        SavedJsonGame() ;
    }
    public static void CardInfoSave(CardInfo cardInfo) => GameManager.Instance.SaveCard.Cards.Add(cardInfo);
    public static void SaveCardIsFlipped(int cardIndex, bool isFlip) => GameManager.Instance.SaveCard.Cards[cardIndex].IsFlipped = isFlip;

    public static void SaveCardIsMatch(int cardIndex, bool isMatch) => GameManager.Instance.SaveCard.Cards[cardIndex].IsMatched = isMatch;
    public static void AddFlipCard (int cardIndex) => GameManager.Instance.SaveCard.FlippedCards.Add(GameManager.Instance.SaveCard.Cards[cardIndex]);
    public static void RemoveFlipCard (CardInfo cardInfo) => GameManager.Instance.SaveCard.FlippedCards.Remove(cardInfo);
    public static CardInfo GetCardInfo (int cardIndex) => GameManager.Instance.SaveCard.FlippedCards[cardIndex];

}