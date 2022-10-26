using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidMan : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Enemy.transform.position + Player.transform.position) / 2;
    }
}
