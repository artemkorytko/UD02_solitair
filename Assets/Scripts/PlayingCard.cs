using UnityEngine;

namespace DefaultNamespace
{
    public class PlayingCard : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Transform openCardContainer;
        [SerializeField] private Transform closeCardContainer;

        private int _value;
        private CardColor _cardColor;
        private CardType _cardType;
        private CardPlays _cardPlays;
        private bool _isOpen;

        public int Value => _value;
        public  CardColor CardColor => _cardColor;
        public CardType CardType => _cardType;
        public CardPlays CardPlays => _cardPlays;
        

        public bool iSiNDask { get; set; } = true;

        public void Initialize(int value, CardColor color, CardType type, Material material)
        {
            _value = value;
            _cardColor = color;
            _cardType = type;
            _meshRenderer.material = material;

            nextCardValue = _value - 1;
            nextCardColor = _color == CardColor.Red ? CardColor.Black : CardColor.Red;
            nextCardType = CardType.Any;
        }

        public void Open()
        {
            if (_isOpen) return;

            _isOpen = true;
            cardContainer = openCardContainer;
            transform.Rotate(Vector3.forward * 180f, Space.Self);
        }

        public void Close()
        {
            if (!_isOpen) return;

            _isOpen = false;
            cardContainer = closeCardContainer;
            transform.Rotate(Vector3.forward * -180f, Space.Self);
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

                if (parent is PlayingCard playingCard)
                {
                    playingCard.Open();
                }

                _parent = parent;
                SetAtMain(parent.IsMain);
            }
        }

        private void SetAtMain(bool state)
        {
            if (state)
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

        public void Reset()
        {
            SetParent();
            Close();
            //
        }
    }
}