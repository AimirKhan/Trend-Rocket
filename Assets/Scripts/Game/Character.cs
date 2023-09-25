using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private ZombieSO zombieData;
        [SerializeField] private Image image;
        [SerializeField] private Button selectButton;
        [SerializeField] private TextMeshProUGUI buttonText;

        [Header("Character parameters")]
        [SerializeField] private float previewScale = 1.0f;
        [SerializeField] private bool isSpriteInversed;

        public float PreviewScale
        {
            get => previewScale;
            set => previewScale = value;
        }

        public ZombieSO ZombieData => zombieData;

        public string ButtonText
        {
            get => buttonText.text;
            set => buttonText.text = value;
        }

        private RectTransform rectTransform;
    
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            rectTransform.sizeDelta = zombieData.Sprite.rect.size * previewScale;
            image.sprite = zombieData.Sprite;
            IsSpriteInversed();
        }

        private void OnEnable()
        {
            selectButton.onClick.AddListener(ChooseCharacter);
        }

        private void ChooseCharacter()
        {
            var score = GlobalVariables.Instance.CurrentScore.Value;
            var zombieCost = zombieData.AccessibleTypes.GoalAmount;

            if (score >= zombieCost)
            {
                GlobalVariables.Instance.CurrentZombie.Value = zombieData.ZombieId;
            }
            else
            {
                Debug.Log("Not enough kilometers");
            }
        }

        private void IsSpriteInversed()
        {
            var localScale = image.transform.localScale;
            if (!isSpriteInversed) return;
            localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            image.transform.localScale = localScale;
        }
    
        private void OnDisable()
        {
            selectButton.onClick.RemoveListener(ChooseCharacter);
        }
    }
}
