using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageController : MonoBehaviour
{
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B)){
            GameObject.Instantiate(bullet);
        }
    }
}
