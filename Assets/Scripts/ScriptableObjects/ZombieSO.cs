using UnityEngine;

namespace ScriptableObjects
{
    public enum EZombieAccessibleType
    {
        Selected,
        Free,
        Locked100Km,
        Locked150Km,
        Locked170Km,
        Locked200Km
    }
    
    [CreateAssetMenu(fileName = "Zombie_", menuName = "Create zombie", order = 52)]
    public class ZombieSO : ScriptableObject
    {
        [SerializeField] private int zombieId;
        [SerializeField] private Sprite sprite;
        [SerializeField] private AccessibleTypes accessibleTypes;
        
        public int ZombieId => zombieId;
        public Sprite Sprite => sprite;
        public AccessibleTypes AccessibleTypes => accessibleTypes;
    }
}