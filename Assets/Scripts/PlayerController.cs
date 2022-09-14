using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private const string LAYER_KEY = "PlayingCard";
        private CardDeck _cardDeck; //ссылка на колоду
        private PlayingCard _holdCard; //ссылка на карту, которую держим
        private Camera camera; //откуда бросаем рейкаст 

        private LayerMask _layerMask;
        private Vector3 _offset;

        public event Action<CardType, int> OnAddToMain; //тип и индекс карты
        public event Action<CardType, int> OnRemoveFromMain;

        private void Awake()
        {
            _cardDeck = FindObjectOfType<CardDeck>(); //заполняем ссылку
            camera = Camera.main; //получаем ссылку на камеру
            _layerMask = LayerMask.GetMask(LAYER_KEY); //получаем ссылку на слой
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) //если нажата кнопка , попытка получить карту
            {
                TryHoldCard();
            }

            if (Input.GetMouseButton(0) && _holdCard != null) //если удерживать кнопку и исть карта
            {
                MoveCardWithMouse();
            }

            if (Input.GetMouseButtonUp(0) && _holdCard != null) //если jпустили кнопку и держим карту
            {
                ReleaseCard();
            }
        }

        private void TryHoldCard()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); // ray from camera
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,
                    _layerMask)) //если рейкаст попадает в объект со слоем PlayingCard
            {
                if (hit.collider.gameObject
                    .TryGetComponent(out PlayingCard card)) //если объект имеет компонент PlayingCard 
                {
                    _holdCard = card;
                    _holdCard.transform.Translate(Vector3.back * 0.5f, Space.World); //сдвинуть карту вперед
                    Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                    var position = _holdCard.transform.position;
                    mousePosition.z = position.z; //взять позицию играющей карты 
                    _offset = position - mousePosition; // разница от позиции взятой карты и мышки получили вектор 
                }
            }
        }

        private void ReleaseCard()
        {
            Vector3 checkPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            checkPosition.z = _holdCard.transform.position.z; //взять позицию играющей карты 

            Ray ray = new Ray(_holdCard.transform.position, Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out CardPlace place)) //пытаемся получить CardPlace
                {
                    if (place.IsCanConnect(_holdCard))
                    {
                        if (place.IsMain)
                        {
                            OnAddToMain?.Invoke(_holdCard.Type, _holdCard.Value);
                        }

                        if (_holdCard.IsMain && !place.IsMain)
                        {
                            OnRemoveFromMain?.Invoke(_holdCard.Type, _holdCard.Value);
                        }

                        _holdCard.SetParent(place);
                        if (_holdCard.IsInDeck)
                        {
                            _cardDeck.ExcludeCurrentCard();
                        }
                    }
                    else
                    {
                        _holdCard.SetParent();
                    }
                }
            }
            else
            {
                _holdCard.SetParent();
            }

            _holdCard = null;
        }

        private void MoveCardWithMouse() //двигать карту за мышью
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = _holdCard.transform.position.z;
            _holdCard.transform.position = mousePosition + _offset;
        }
    }
}