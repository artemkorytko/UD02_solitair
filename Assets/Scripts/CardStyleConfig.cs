using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CardStyleConfig", menuName = "MENUNAME", order = 0)]
    public class CardStyleConfig : ScriptableObject
    {
        [SerializeField] private Material[] Diamonds;
        [SerializeField] private Material[] Hearts;
        [SerializeField] private Material[] Cubes;
        [SerializeField] private Material[] Spades;
        
    }
}