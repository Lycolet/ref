using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IShotable/Create KineticBurst", fileName = "KineticBurst")]
[System.Serializable]
public class KineticBurst : BaseBarrage
{
    public override GameObject[] Shot()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject obj in objects)
        {
            Rigidbody2D rigidbody2 = obj.GetComponent<Rigidbody2D>();
            rigidbody2.isKinematic = false;
            rigidbody2.AddRelativeForce(new Vector2(Random.Range(-100f, 100f), 100f));
        }
        return null;
    }
}
