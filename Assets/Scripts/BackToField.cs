using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CanvasManager.Instance.UnloadCanvas("RestaurantCanvas");
            CanvasManager.Instance.LoadCanvas("FieldCanvas");
            //SceneManager.LoadScene("FungiField");
        }
    }
}
