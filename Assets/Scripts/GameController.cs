using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int mainPlaceCompletedValue = 52;
    [SerializeField] private CardPlace[] gameCardPlaces;

    private CardDeck _cardDeck;
    private PlayerController _playerController;
    private readonly Dictionary<CardType, int> _mainPlaceInfo = new Dictionary<CardType, int>();

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

    public void Reset()
    {
        _cardDeck.Reset();
        _cardDeck.RandomizeDeck();
        FillGamePlaces();
        _mainPlaceInfo.Clear();
    }

    private void OnAddToMain(CardType type, int value)
    {
        if (_mainPlaceInfo.ContainsKey(type))
        {
            _mainPlaceInfo[type] += value;
        }
        else
        {
            _mainPlaceInfo.Add(type, value);
        }

        CheckMainPlaces();
    }

    private void OnRemoveMain(CardType type, int value)
    {
        if (_mainPlaceInfo.ContainsKey(type))
        {
            _mainPlaceInfo[type] -= value;
        }
    }
    
    private void CheckMainPlaces()
    {
        var keys = _mainPlaceInfo.Keys;
        
        if (keys.Count < 4)
        {
            return;
        }

        bool isWin = true;

        foreach (var key in _mainPlaceInfo.Keys)
        {
            if (_mainPlaceInfo[key] != mainPlaceCompletedValue)
            {
                isWin = false;
                break;
            }
        }

        if (isWin)
        {
            OnWin?.Invoke();
        }
    }
}
