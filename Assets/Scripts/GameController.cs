using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int mainPlaceCompleteValue = 52; //сколько нужно собрать карт, чтобы засчиталась победа
        [SerializeField] private CardPlace[] gameCardPlaces; //массив плесов

        private CardDeck _cardDeck; //ссылка на колоду
        private PlayerController _playerController; //ссылка на игрока
        private readonly Dictionary<CardType, int> _mainPlaceInfo = new Dictionary<CardType, int>(); 

        public event  Action OnWin;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();//запполняем ссылку
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
            _cardDeck.Initialize(); //сгенерировать колоду
            FillGamePlaces(); //заполнить игровое пространство
        }

        private void FillGamePlaces()
        {
            for (int i = 0; i < gameCardPlaces.Length; i++)
            {
                int counter = i;
                CardPlace cardPlace = gameCardPlaces[i]; //ссылка на текущий кардплейс
                PlayingCard card = null; //временную ссылку, которую получаем из колоды
                
                //предыдущие карты будут закрыты
                while (counter > 0)
                {
                    card = _cardDeck.GetCard(); //получить карту из колоды
                    card.SetParent(cardPlace); //устанавливаем родителя
                    card.Close();//
                    cardPlace = card; //обновляем крдплейс -  следующую карту кладем на предыдущую
                    counter--;//уменьшаем счетчик 
                }
                //последние карты будут открыта
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

        private void OnRemoveFromMain(CardType type, int value)
        {
            if (_mainPlaceInfo.ContainsKey(type))
            {
                //_mainPlaceInfo(type) += value;
            }
            else
            {
                _mainPlaceInfo.Add(type,value);
            }

            CheMainPLaces();
        }

        private void CheMainPLaces()
        {
            var keys = _mainPlaceInfo.Keys;
            if (keys.Count < 4)
            {
                return;
            }

            bool isWin;

            foreach (var key in _mainPlaceInfo.Keys)
            {
                if (_mainPlaceInfo[key] != mainPlaceCompleteValue)
                {
                    isWin = false;
                    break;
                }
            }
        }

        private void OnAddToMain(CardType type, int value)
        {
        }
    }
}