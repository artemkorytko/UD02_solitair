using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : CardPlace
    {
        private const string 
        private CardDeck cardDeck;
        private PlayingCard playingCard;
        private Camera camera;

        private LayerMask layerMask;

        public event Action<CardType, int> OnAddmain;
        public event Action<CardType, int> OnRemoveFromMain;

        private void Awake()
        {
            cardDeck = FindObjectOfType<CardDeck>();
            camera = Camera.main;
            layerMask = LayerMask.GetMask(LAYER_KEY);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //
            }

            if (Input.GetMouseButton(0) && _holdCard != null)
            {
                //
            }

            if (Input.GetMouseButtonUp(0) && _holdCard != 0)
            {
                //
            }
        }

        private void TryHoldCard()
        {
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out PlayingCard card))
                {
                    _holdCard = card;
                    _holdCard.transform.Translate(Vector3.back * 0.5f, Space.World);
                }
            }
        }

        private void ReleaseCard()
        {
            //
        }
    }
}