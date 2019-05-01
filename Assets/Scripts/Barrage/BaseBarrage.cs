using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBarrage : MonoBehaviour {

    [SerializeField] private int localTime;
    public int LocalTime { get { return localTime; } }
    [SerializeField] protected Sprite bulletImage;
    [SerializeField] protected float magnificationRate = 1;
    [SerializeField] protected Color bulletColor = new Color(1,1,1,1);
    [SerializeField] protected bool shotFromEnemy = true;
    [SerializeField] protected Vector2 startPoint;
    [SerializeField] protected bool aimForPlayer;
    [SerializeField] protected float shotAngle;//*PI
    [SerializeField] protected float speed;

    protected GameObject bullet;

    //#############################################################################################

    /// <summary>
    /// 呼び出し用？弾幕の種類にかかわらず固有の処理
    /// </summary>
    /// <param name="shooter">弾を撃つ敵オブジェクト</param>
    /// <returns></returns>
    public virtual GameObject[] Shot()
    {
        bullet = Pool.GetObjectPool((GameObject)Resources.Load("Bullet")).GetInstance();//インスタンス取り出し
        SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();//弾の見た目の変更
        sprite.sprite = bulletImage;
        sprite.color = bulletColor;
        bullet.transform.position = startPoint + (shotFromEnemy ? (Vector2)transform.position : Vector2.zero);//弾を発射位置に移動
        Vector2 player = PlayerController.Instance.transform.position;
        float dx = player.x - bullet.transform.position.x;
        float dy = player.y - bullet.transform.position.y;
        float angle = (shotAngle * Mathf.PI) + (aimForPlayer ? Mathf.Atan2(dy, dx) : 3 * Mathf.PI / 2);//初速の角度を計算
        Vector2 deltaTranslate = (speed / 1000) * (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));//初速 = 速度(/1000) * 単位ベクトル
        bullet.GetComponent<BulletBehavior>().velocity = deltaTranslate;//弾に初速を与える。
        return new GameObject[1] { bullet };
    }
}
