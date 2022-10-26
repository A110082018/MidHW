using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMan : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        
        if (dist < 6)
        {
            transform.localScale = new Vector3(2,2,2);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
        
    }
}
