using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [System.Serializable] public class Movement   //キャラの移動を記述
    {
        [SerializeField] public int relativeFrame;//相対時間
        [SerializeField] public bool relative;//相対距離指定？
        [SerializeField] public Vector2 destination;//目的地or相対距離
        [SerializeField] public int costFrame;//時間

        /// <summary>
        /// 移動する
        /// </summary>
        /// <param name="transform">Enemyオブジェクト</param>
        /// <returns></returns>
        public IEnumerator Move(Transform transform)
        {
            Vector2 deltaDistance = (relative ? destination : (destination - (Vector2)transform.position)) / costFrame;
            for(int i = 0; i < costFrame; i++)
            {
                transform.Translate(deltaDistance);
                yield return null;
            }
            yield break;
        }
    }

    //##############################################################################################

    [SerializeField] int hp;
    [SerializeField] int despawnFrame;//デスポーンするフレーム
    [SerializeField] bool loop;//弾幕、移動のループ
    [SerializeField] int loopFrame;//何フレーム目にループするか
    [SerializeField] public List<Movement> movements;
    public List<BaseBarrage> barrages;

    private int localFrame = 0;//キャラの登場後の時間

    //##################################################################################################

    // Use this for initialization
    void Start () {
        LoadBarrages();
    }
    
    // Update is called once per frame
    void Update () {
        TaskRun();
        DespawnCheck();
        LocalFrameUpdate();
    }

    /// <summary>
    /// 当たり判定に引っかかったときに呼び出されるやつ
    /// </summary>
    public void Damage()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    //###################################################################################################

    /// <summary>
    /// 弾幕のリストを初期化
    /// </summary>
    private void LoadBarrages()
    {
        barrages.AddRange(GetComponents<BaseBarrage>());
        barrages.Sort((a, b) => a.LocalTime - b.LocalTime);
    }

    /// <summary>
    /// 現在のlocalFrameに、移動・弾幕の発射が予約されているか確認、されていれば実行する。
    /// </summary>
    private void TaskRun()
    {
        List<Barrage> loadingBarrages = barrages.FindAll(x => x.relativeFrame == localFrame);
        foreach (Barrage x in loadingBarrages)
        {
            x.barrage.Shot(gameObject);
        }
        List<Movement> loadingMoves = movements.FindAll(x => x.relativeFrame == localFrame);
        foreach (Movement x in loadingMoves)
        {
            StartCoroutine(x.Move(transform));
        }
    }

    /// <summary>
    /// デスポーンする
    /// </summary>
    private void DespawnCheck()
    {
        if (localFrame >= despawnFrame)
            Destroy(gameObject);
    }

    /// <summary>
    /// キャラ固有時間(localFrame)を加算、ループ
    /// </summary>
    private void LocalFrameUpdate()
    {
        localFrame++;
        if (loop == true && localFrame > loopFrame)
            localFrame = 0;
    }
}
