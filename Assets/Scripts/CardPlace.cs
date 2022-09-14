using UnityEngine;

namespace DefaultNamespace
{
    public class CardPlace : MonoBehaviour
    {
        [SerializeField] protected int nextCardValue = -1; //индекс следующей карты
        [SerializeField] protected float onGameZOffset = -2f;

        [SerializeField]
        protected CardColor nextCardColor = CardColor.Any; //цвет карты который можем положить - любой по умолчанию

        [SerializeField] protected CardType nextCardType = CardType.Any; //тип - -любой по  умолчанию
        [SerializeField] protected Transform cardContainer; //трансфор, относительно его становится карта
        [SerializeField] protected bool isMain; // главная ли площадка, тру у главных 4х карт

        protected bool _isOpen = true; //открыта ли площадка

        public int NextCardValue => nextCardValue;

        public CardColor NextCardColor => nextCardColor;

        public CardType NextCardType => nextCardType;

        public Transform CardContainer => cardContainer;

        public bool IsMain => isMain;

        public bool IsOpen => _isOpen;

       
        public bool IsCanConnect(PlayingCard playingCard) //можно ли положить карту PlayingCard playingCard на площадку 
        {
            if (!_isOpen) //нельзя положить, если не открыта
                return false;

            if (playingCard.Value != nextCardValue &&
                nextCardValue != -1) //индекс текущей карты не равен индексу, который ждем и  текущая карта не свободна
                return false;

            if (nextCardColor != CardColor.Any && playingCard.Color != nextCardColor) // сравноние по цвветц 
                return false;

            if (nextCardType != CardType.Any && playingCard.Type != nextCardType) // сравнение по масти
                return false;

            Vector3 position = playingCard.CardContainer.localPosition; //кладем карту в позицию контейнера
            position.z =
                isMain
                    ? 0f
                    : onGameZOffset; //верхняя карта-главная площадка? тогда позиция 0, иначе позиция onGameZOffset
            playingCard.CardContainer.localPosition = position; //текущей карте присваиваем эту позицию 
            return true;
        }
    }
}