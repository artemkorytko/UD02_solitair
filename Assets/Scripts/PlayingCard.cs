using UnityEngine;

namespace DefaultNamespace
{
    public class PlayingCard : CardPlace
    {
        [SerializeField] private MeshRenderer meshRenderer; //доступ к материалам в мешрендер
        [SerializeField] private Transform openCardContainer; //положение закрытых карт
        [SerializeField] private Transform closeCardContainer; //положение закрытых карт

        private int _value;
        private CardColor _color; 
        private CardType _type; //масть
        private CardPlace _parent; //куда перетаскиваем карту

        public int Value => _value;

        public CardColor Color => _color;

        public CardType Type => _type;

        public CardPlace Parent => _parent;

        public bool IsInDeck { get; set; } = true; // являемся ли частью колоды или карта на доске и доступна для игры

        public void Initialize(int value, CardColor color, CardType type, Material material)
        {
            _value = value;
            _color = color;
            _type = type;
            meshRenderer.material = material; //инициализация себя

            nextCardValue = _value - 1; //можем положить карту ниже нас достоинством
            nextCardColor = _color == CardColor.Red ? CardColor.Black : CardColor.Red; // если цвет нашей карты красный на себя можем положить черный
            nextCardType = CardType.Any; //инициализация следующей карты
        }

        public async void Open()
        {
            if (_isOpen) return;

            _isOpen = true;
            cardContainer = openCardContainer; //расположение открытой карты 
            transform.Rotate(Vector3.forward * 180f, Space.Self);
        }

        public async void Close()
        {
            if (!_isOpen) return;

            _isOpen = false;
            cardContainer = closeCardContainer; //расположение закрытой карты
            //можно сделать анимацию DOTween and Async - await добавить 
            transform.Rotate(Vector3.forward * -180f, Space.Self); //рубахой вверх 
        }

        public void SetParent(CardPlace parent = null)
        {
            if (parent == null)
            {
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.SetParent(parent.CardContainer);
                transform.localPosition = Vector3.zero;
                SetAtMain(parent.IsMain);
                if (parent is PlayingCard playingCard) //если стаавим на игравую карту
                {
                    playingCard.Open();
                }
                _parent = parent;
                //SetAtMain(parent.IsMain);
            }
        }

        private void SetAtMain(bool state) //являемся ли мы главной картой
        {
            if (state) //если главная карта
            {
                nextCardColor = _color;
                nextCardType = _type;
                nextCardValue = _value + 1;
            }
            else
            {
                nextCardValue = _value - 1;
                nextCardColor = _color == CardColor.Red ? CardColor.Black : CardColor.Red;
                nextCardType = CardType.Any;
            }

            isMain = state;
        }

        public void Reset() //сброс карт
        {
            SetParent();
            Close();
            SetAtMain(false);
            _parent = null;
        }
    }
}