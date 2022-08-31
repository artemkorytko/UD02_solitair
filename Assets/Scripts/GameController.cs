using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int mainPlaceCompleteValue = 52;
        [SerializeField] private CardPlace[] gameCardPlace; 
        
        private CardDeck cardDeck;
        private PlayerController playerController;
        private readonly Dictionary<CardType, int> _mainPlayceInfo = new Dictionary<CardType, int>();

        private void Start()
        {
            GenerateField();
            _playerController.OnAddToMain += OnAddToMain;
            _playerController.OnRemoveFromMain += OnRemoveFromMain;
        }

        private void GenerateField()
        {
            //
        }

        private void FillGamePlaces()
        {
            //
        }
    }
}