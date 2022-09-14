using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    public class CardDeck : MonoBehaviour
    {
        [SerializeField] private PlayingCard cardPrefab; //игровая карта
        [SerializeField] private CardStyleConfig cardConfig; //конфиг всех карт 

        [SerializeField]
        private Transform showContainer; // ссылка на то место, рядом с колодой, куда скажем достать карту из колоды

        private readonly List<PlayingCard> _cards = new List<PlayingCard>(); //храним текущие карты
        private readonly List<PlayingCard> _allCards = new List<PlayingCard>(); //храним все карты

        private int _currentShown = -1; // карта по очереди
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>(); //заполнили сссылку
        }

        public void Initialize()
        {
            GenerateCards();
            _allCards.AddRange(
                _cards); //созданный массив добавили в массив всех кард , для ссверки - собраны ли все карты
            RandomizeDeck();
        }

        private void GenerateCards()
        {
            GenerateType(CardType.Diamonds, CardColor.Red, cardConfig.Diamonds);
            GenerateType(CardType.Hearts, CardColor.Red, cardConfig.Hearts);
            GenerateType(CardType.Spades, CardColor.Black, cardConfig.Spades);
            GenerateType(CardType.Clubs, CardColor.Black, cardConfig.Clubes);
        }

        private void GenerateType(CardType type, CardColor color, Material[] materials) //генерация карты
        {
            for (int i = 0; i < materials.Length; i++)
            {
                PlayingCard
                    newCard = Instantiate(cardPrefab,
                        showContainer); //создаем карту, и положение карты(новый родитель CardPlace)
                newCard.Initialize(i, color, type, materials[i]); //инициализация новой карты
                newCard.gameObject.SetActive(false);
                newCard.Close(); //переворачиваем карту
                _cards.Add(newCard); //добавляем в массив всех карт
            }
        }

        public void RandomizeDeck() //перемешать колоду
        {
            List<PlayingCard> tempList = new List<PlayingCard>(); //временный лист
            while (_cards.Count > 0) //пока есть карты в колоде
            {
                int randomIndex = Random.Range(0, _cards.Count); //рандомная  карта 
                tempList.Add(_cards[randomIndex]); //во временный лист добавляем карту из колоды 
                _cards.RemoveAt(randomIndex); //удаляем из колоды карт
            }

            _cards.AddRange(tempList); //добавляем рандомный-перетусованный лист в колоду
        }

        public PlayingCard GetCard() //вернет первую карту 
        {
            if (_cards.Count == 0) return null;

            PlayingCard card = _cards[0]; //получаем 1ую карту из колоды
            _cards.Remove(card); //удаляем
            card.gameObject.SetActive(true); //включаем карту
            card.IsInDeck = false; //теперь карта не в колоде
            return card;
        }

        public void ExcludeCurrentCard() //
        {
            _cards[_currentShown].IsInDeck = false; //карта больше не в колоде
            _cards.RemoveAt(_currentShown); //удалить карту 

            if (_currentShown + 1 < _cards.Count)
            {
                _currentShown++;
            }
            else
            {
                _meshRenderer.enabled = false;
                _currentShown = -1;
            }
        }

        private void
            OnMouseUpAsButton() //метод юнити , определяет что мы кликнули на объект у которого есть коллайдер и 
        {
            ShowNext();
        }

        private void ShowNext()
        {
            if (_currentShown >= 0)
            {
                _cards[_currentShown].gameObject.SetActive(false); //выключить передыдущую карту
                _cards[_currentShown].Close(); //закрыть
            }

            _currentShown++; //увеличиваем индекс

            if (_currentShown == _cards.Count - 1 &&
                _meshRenderer
                    .enabled) //если дошли до последней карты (индекс = индексу последней карты)  и колода включена
            {
                _cards[_currentShown].gameObject.SetActive(true); //показываем  карту последнюю в колоде
                _cards[_currentShown].Open(); //открываем карту логически
                _meshRenderer.enabled = false; // выключаем отображение колоду
            }
            else if (_currentShown >= _cards.Count) // если индекс стал больше карт в колоде
            {
                _currentShown = -1; //сброс индекса
                _meshRenderer.enabled = true; //включить колоду
            }
            else
            {
                _cards[_currentShown].gameObject.SetActive(true); //делать ход
                _cards[_currentShown].Open();
            }
        }

        public void Reset()
        {
            _cards.Clear(); //чистим карты
            for (int i = 0; i < _allCards.Count; i++)
            {
                _allCards[i].transform.SetParent(null); //сброс родителей карт
            }

            for (int i = 0; i < _allCards.Count; i++)
            {
                _allCards[i].transform.SetParent(showContainer);
                _allCards[i].Reset();
                _allCards[i].IsInDeck = true;
                _allCards[i].gameObject.SetActive(false);
            }

            _cards.AddRange(_allCards);
        }
    }
}