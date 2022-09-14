using UnityEngine;

namespace DefaultNamespace.Shop
{
    [CreateAssetMenu(fileName = "StoreItem", menuName = "Config/Store/StorItem", order = 0)]
    public class StorItem : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private float price;
        [SerializeField] private string title;
        [SerializeField] private string description;

        [SerializeField] private Sprite icon;
        
        
    }
}