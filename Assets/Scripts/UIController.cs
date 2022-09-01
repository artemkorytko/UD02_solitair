using System;
using DefaultNamespace.Ads;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIController : MonoBehaviour
    {
        private GameController _gameController;

        [SerializeField] private GameObject game;
        [SerializeField] private GameObject win;

        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            _gameController.OnWin += OnWin;
            OnGame();
        }

        private void OnDestroy()
        {
            _gameController.OnWin -= OnWin;
        }

        private void OnWin()
        {
            game.SetActive(false);
            win.SetActive(true);
        }
        
        public void OnGame()
        {
            win.SetActive(false);
            game.SetActive(true);
            FindObjectOfType<AdsInitializer>().ShowAd();
        }
    }
}