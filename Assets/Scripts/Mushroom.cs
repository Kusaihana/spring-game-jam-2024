using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Mushroom : MonoBehaviour, ITouchable, ICollectable
    {
        public MushroomType type;
        public int amount;
        public bool isGlowing;
        public float spawnProbability;
        
        public GameObject informationPrompt;

        private GameObject informationPromptInstance;

        public void ShowPrompt()
        {
            TextMeshProUGUI textField = informationPrompt.GetComponentInChildren<TextMeshProUGUI>();
            textField.text = MushroomTypeNames.myEnumDescriptions[type];
            informationPromptInstance = InfoDisplay.Instance.AddUIElement(informationPrompt);
        }

        public void HidePrompt()
        {
            if(informationPromptInstance)
                Destroy(informationPromptInstance);
        }

        public CollectionItem Collect()
        {
            Destroy(gameObject.transform.parent.gameObject);
            return new CollectionItem(type, amount);
        }
    }
}