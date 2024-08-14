using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card : MonoBehaviour
{
    public Image CardImage { get; private set; }
    public Button CardButton { get; private set; }
    public Sprite FrontSprite { get; private set; }
    public Sprite BackSprite { get; private set; }

    private bool isFlipped;
    private bool isMatched;
    private GameManager gameManager;

    private void Awake()
    {
        CardImage = GetComponent<Image>();
        CardButton = GetComponent<Button>();
        CardButton.onClick.AddListener(OnCardClicked);
    }

    public void Initialize(Sprite frontSprite, Sprite backSprite, GameManager manager)
    {
        FrontSprite = frontSprite;
        BackSprite = backSprite;
        gameManager = manager;
        ResetCard();
    }

    public void ResetCard()
    {
        isFlipped = false;
        isMatched = false;
        CardImage.sprite = BackSprite;
        CardButton.interactable = true;
    }

    private void OnCardClicked()
    {
        if (isFlipped || isMatched) return;

        gameManager.OnCardSelected(this);
        FlipCard();
    }

    public void FlipCard()
    {
        isFlipped = true;
        CardButton.interactable = false;

        CardImage.transform.DOScaleX(0, 0.2f).OnComplete(() =>
        {
            CardImage.sprite = FrontSprite;
            CardImage.transform.DOScaleX(1, 0.2f);
        });
    }

    public void FlipBack()
    {
        isFlipped = false;
        CardButton.interactable = true;

        CardImage.transform.DOScaleX(0, 0.2f).OnComplete(() =>
        {
            CardImage.sprite = BackSprite;
            CardImage.transform.DOScaleX(1, 0.2f);
        });
    }

    public void SetMatched()
    {
        isMatched = true;
        CardButton.interactable = false;

        CardImage.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1);
    }

    public bool IsMatched() => isMatched;
    public bool IsFlipped() => isFlipped;
}
