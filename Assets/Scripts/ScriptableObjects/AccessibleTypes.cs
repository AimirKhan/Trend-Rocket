using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "AccessibleType_", menuName = "Create Accessible Type", order = 51)]
    public class AccessibleTypes : ScriptableObject
    {
        [SerializeField] private EZombieAccessibleType accessibleType;
        [SerializeField] private int goalAmount;
        [SerializeField] private string valueText;

        public EZombieAccessibleType AccessibleType => accessibleType;
        public int GoalAmount => goalAmount;
        public string ValueText => valueText;
    }
}