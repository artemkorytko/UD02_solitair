using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class UiController : MonoBehaviour
    {
        private GameController _gameController;
        [SerializeField] private GameObject game;
        [SerializeField] private GameObject win;

        private void Awake()
        {
            
        }

        private void OnDestroy()
        {
            
        }

        public void onWin()
        {
            
        }
    }
}