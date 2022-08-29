using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int mainPlaceCompletedValue = 52;
    [SerializeField] private CardPlace[] gameCardPlaces;
    
    private CardDeck _cardDeck;
    private PlayerController _playerController;
    private readonly Dictionary<CardType, int> mainPlaceInfo = new Dictionary<CardType, int>();

    public event Action OnWin;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _cardDeck = FindObjectOfType<CardDeck>();
    }

    private void Start()
    {
        GenerateField();
        _playerController.OnAddToMain += OnAddToMain;
        _playerController.OnRemoveFromMain += OnRemoveMain;
    }

    private void OnDestroy()
    {
        _playerController.OnAddToMain -= OnAddToMain;
        _playerController.OnRemoveFromMain -= OnRemoveMain;
    }

    private void GenerateField()
    {
        _cardDeck.Initialize();
        FillGamePlaces();
    }

    private void FillGamePlaces()
    {
        for (var i = 0; i < gameCardPlaces.Length; i++)
        {
            var counter = i;
            CardPlace cardPlace = gameCardPlaces[i];
            PlayingCard card = null;

            while (counter > 0)
            {
                card = _cardDeck.GetCard();
                card.SetParent(cardPlace);
                cardPlace = card;
                counter--;
            }

            card = _cardDeck.GetCard();
            card.SetParent(cardPlace);
            card.Open();
        }
    }
    
    private void OnAddToMain(CardType type, int value)
    {
        throw new NotImplementedException();
    }
    
    private void OnRemoveMain(CardType type, int value)
    {
        throw new NotImplementedException();
    }
}
