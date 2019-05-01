using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public Vector2 velocity;

    //##############################################################################

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        BulletDespawnCheck();
        BulletTranslate();
    }

    //##############################################################################

    /// <summary>
    /// 弾を移動させる
    /// </summary>
    private void BulletTranslate()
    {
        transform.Translate(velocity);
        transform.Rotate(velocity);
    }

    /// <summary>
    /// 範囲外に出れば弾を消す
    /// </summary>
    private void BulletDespawnCheck()
    {
        if (GameManager.Instance.InRange(transform.position) == false)
        {
            gameObject.SetActive(false);
        }
    }
}
