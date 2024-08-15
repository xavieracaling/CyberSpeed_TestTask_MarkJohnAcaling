using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card : MonoBehaviour
{
    public Image CardImage { get; private set; }
    public Button CardButton ;
    public Sprite FrontSprite { get; private set; }
    public Sprite BackSprite { get; private set; }
    public GameObject FrontSpriteGO { get; private set; }

    public bool IsFlipped;
    public bool IsMatched;
    public int CardIndex;
    public AudioSource PageSFX;
    public AudioSource MatchedSFX;
    public AudioSource NotMatchedSFX;

    public void Initialize(Sprite frontSprite, Sprite backSprite)
    {
        PageSFX = gameObject.AddComponent<AudioSource>();
        PageSFX.volume = 0.4f;
        PageSFX.clip = AudioManager.Instance.PageFlip;
        
        MatchedSFX = gameObject.AddComponent<AudioSource>();
        MatchedSFX.volume = 0.4f;
        MatchedSFX.clip = AudioManager.Instance.Matched;

        NotMatchedSFX = gameObject.AddComponent<AudioSource>();
        NotMatchedSFX.volume = 0.4f;
        NotMatchedSFX.clip = AudioManager.Instance.NotMatched;

        CardImage = GetComponent<Image>();
        CardButton = GetComponent<Button>();
        CardButton.onClick.AddListener(OnCardClicked);
       

        CardIndex = GameManager.Instance.CurrentCarDeck.Cards.IndexOf(this);
        FrontSprite = frontSprite;
        BackSprite = backSprite;
        FrontSpriteGO = transform.GetChild(0).gameObject;
    }

    private void OnCardClicked()
    {
        if (IsFlipped || IsMatched) return;

        GameManager.Instance.OnCardSelected(this);
        FlipCard();
        
    }

    public void FlipCard()
    {
        PageSFX.Play();
        IsFlipped = true;
        CardButton.interactable = false;
        CardProcessSaving.SaveCardIsFlipped(CardIndex,IsFlipped);
        CardImage.transform.DOScaleX(0, 0.2f).OnComplete(() =>
        {
            FrontSpriteGO.gameObject.SetActive(true);
            CardImage.transform.DOScaleX(1, 0.2f);
        });
        CardProcessSaving.SavedJsonGame();
    }


    public void FlipBack()
    {
        NotMatchedSFX.Play();
        IsFlipped = false;
        CardButton.interactable = true;
        CardProcessSaving.SaveCardIsFlipped(CardIndex,IsFlipped);
        CardImage.transform.DOScaleX(0, 0.2f).OnComplete(() =>
        {
            FrontSpriteGO.gameObject.SetActive(false);
            CardImage.sprite = BackSprite;
            CardImage.transform.DOScaleX(1, 0.2f);
        });
        CardProcessSaving.SavedJsonGame();
    }

    public void SetMatched()
    {
        IsMatched = true;
        CardButton.interactable = false;

        CardImage.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1);
    }

}
