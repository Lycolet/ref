using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 10;
    private Vector2 vec;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 enemypos = transform.position;
        Vector2 playerpos = player.GetComponent<Transform>().position;
        //Vector2 diff = enemypos - 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vec * speed);
    }
}
