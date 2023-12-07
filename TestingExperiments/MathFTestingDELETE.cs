using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathFTestingDELETE : MonoBehaviour
{
    [SerializeField] Text textBox;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float testValue = Mathf.Repeat(22, 9f);
        
      
           
            textBox.text = testValue.ToString();
        
        
    }
}
