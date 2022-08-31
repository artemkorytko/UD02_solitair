using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    public class CardDeck : MonoBehaviour
    {
        [SerializeField] private PlayingCard cardPrefab;
        [SerializeField] private CardStyleConfig cardConfig;
        [SerializeField] private Transform showContainer;

        private readonly List<PlayingCard> _cards = new List<PlayingCard>();
        private readonly List<PlayingCard> _allCards = new List<PlayingCard>();

        private int _currentShown = -1;
        private MeshRenderer me;

        private void Awake()
        {
            me = GetComponent<MeshRenderer>();
        }

        public void Initialize()
        {
            GenerateCards();
            _allCards.AddRange(_cards);
            RandomizeDeck();
        }

        private void GenerateCards()
        {
           // GenerateType(CardType.Diamonds, CardColor. );
        }

        private void GenerateType(CardType cardType, CardColor cardColor, Material[] materials)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                PlayingCard newCard = Instantiate(cardPrefab, showContainer);
                newCard.Initialize(i, cardColor, cardType, materials[i]);
                newCard.gameObject.SetActive(false);
                newCard.Close();
                _cards.Add(newCard);
            }
        }

        private void RandomizeDeck()
        {
            List<PlayingCard> tempList = new List<PlayingCard>();
            while ( _cards.Count > 0)
            {
                int randomIndex = Random.Range(0, _cards.Count);
                tempList.Add(_cards[randomIndex]);
                _cards.RemoveAt(randomIndex);
            }
            _cards.AddRange(tempList);
        }

        public PlayingCard GetCard()
        {
            if (_cards.Count == 0) return null;

            PlayingCard card = _cards[0];
            _cards.Remove(card);
            card.gameObject.SetActive(false);
            card.iSiNDask = false;
            return card;
        }

        public void ExcludeCurrentCard()
        {
            _cards[_currentShown].iSiNDask = false;
            _cards.RemoveAt(_currentShown);
            //write
        }

        private void OnMouseUpAsButton()
        {
            ShowNext();
        }

        private void ShowNext()
        {
            if (_currentShown >= 0)
            {
                _cards[_currentShown].gameObject.SetActive(false);
                _cards[_currentShown].Close();
            }

            _currentShown++;

            if (_currentShown == _cards.Count - 1 && me.enabled)
            {
                _cards[_currentShown].gameObject.SetActive(true);
                _cards[_currentShown].Open();
                me.enabled = true;
            }

            else if (_currentShown >= _cards.Count)
            {
                _currentShown = -1;
                me.enabled = true;
            }

            else
            {
                _cards[_currentShown].gameObject.SetActive(true);
                _cards[_currentShown].Open();
            }
        }

        public void Reset()
        {
            _cards.Clear();
            for (int i = 0; i < _allCards.Count; i++)
            {
                _allCards[i].transform.SetParent(null);
            }

            for (int i = 0; i < _allCards.Count; i++)
            {
                _allCards[i].transform.SetParent(showContainer);
                _allCards[i].Reset();
                _allCards[i].iSiNDask = true;
                //_allCards[i].gameObject.SetActive(true);
                //write
            }
        }
    }
}