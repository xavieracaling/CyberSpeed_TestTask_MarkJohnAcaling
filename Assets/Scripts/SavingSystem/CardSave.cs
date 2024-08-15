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
    public static void SavedJsonGame() => PlayerPrefs.SetString("LevelSaved", JsonConvert.SerializeObject(GameManager.Instance.SaveCard) );
    public static void SaveGameInfo()
    {
        GameManager.Instance.SaveCard.CurrentLevel = GameManager.Instance.Level;
        GameManager.Instance.SaveCard.Matches = GameManager.Instance.Matches;
        GameManager.Instance.SaveCard.Turns = GameManager.Instance.Turns;
    }
    public static void CardInfoSave(CardInfo cardInfo) => GameManager.Instance.SaveCard.Cards.Add(cardInfo);
    public static void SaveCardIsFlipped(int cardIndex, bool isFlip)
    {
        
    }
    public static void SaveCardIsMatch(int cardIndex, bool isMatch)
    {
        
    }
}