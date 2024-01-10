using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_Functions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)){
        
          transform.localScale = new Vector3(1, 0.5f, 1);
        }

        if(Input.GetKeyUp(KeyCode.LeftControl)){
          transform.localScale = new Vector3(1, 1, 1);
        }
        
    }
}
