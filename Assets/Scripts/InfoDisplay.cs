using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
   public static InfoDisplay Instance;

   private void Awake() 
   { 
      // If there is an instance, and it's not me, delete myself.
    
      if (Instance != null && Instance != this) 
      { 
         Destroy(this); 
      } 
      else 
      { 
         Instance = this; 
      } 
   }
   public GameObject AddUIElement(GameObject uiElementPrefab)
   {
      return Instantiate(uiElementPrefab, this.transform);
   }
   
}
