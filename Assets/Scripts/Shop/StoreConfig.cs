using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Shop
{
    [CreateAssetMenu(fileName = "StoreConfig", menuName = "Configs/Store/StoreConfig", order = 0)]
    public class StoreConfig : ScriptableObject
    {
        [SerializeField] private StorItem[] items;


        public StorItem GetItemByID(int id)
        {
            return items.FirstOrDefault(item => item.ID == id);
        }

        public void Buy(int id)
        {
            PlayerPrefs.SetInt();
        }
    }
}