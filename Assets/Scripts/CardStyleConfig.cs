using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CardStyleConfig", menuName = "Configs/CardStyleConfig", order = 0)]
    public class CardStyleConfig : ScriptableObject
    {
        [SerializeField] private Material[] diamonds;
        [SerializeField] private Material[] hearts;
        [SerializeField] private Material[] clubes;
        [SerializeField] private Material[] spades;
        public Material[] Diamonds => diamonds;
        public Material[] Hearts => hearts;
        public Material[] Clubes => clubes;
        public Material[] Spades => spades;
    }
}