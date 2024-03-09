using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Mushroom : MonoBehaviour, ITouchable, ICollectable
    {
        public string collectionName;
        public int amount;
        public bool isGlowing;
        
        public GameObject informationPrompt;

        private GameObject informationPromptInstance;

        public void ShowPrompt()
        {
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
            return new CollectionItem(collectionName, amount);
        }
    }
}