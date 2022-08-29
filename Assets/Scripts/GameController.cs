using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int mainPlaceCompleteValue = 52;
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
            _playerController.OnRemoveFromMain += OnRemoveFromMain;
        }

        private void OnDestroy()
        {
            _playerController.OnAddToMain -= OnAddToMain;
            _playerController.OnRemoveFromMain -= OnRemoveFromMain;
        }

        private void GenerateField()
        {
            _cardDeck.Initialize();
            FillGamePlaces();
        }

        private void FillGamePlaces()
        {
            for (int i = 0; i < gameCardPlaces.Length; i++)
            {
                int counter = i;
                CardPlace cardPlace = gameCardPlaces[i];
                PlayingCard card = null;

                while (counter > 0)
                {
                    card = _cardDeck.GetCard();
                    card.SetParent(cardPlace);
                    card.Close();
                    cardPlace = card;
                    counter--;
                }

                card = _cardDeck.GetCard();
                card.SetParent(cardPlace);
                card.Open();
            }
        }

        private void OnRemoveFromMain(CardType type, int value)
        {
        }

        private void OnAddToMain(CardType type, int value)
        {
        }
    }
}