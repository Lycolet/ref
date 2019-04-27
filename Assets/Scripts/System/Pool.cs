using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    public GameObject prefab;
    public int maxCount = 100;
    public int prepareCount = 0;
    [SerializeField]
    private int interval = 1;
    private List<GameObject> pooledObjectList = new List<GameObject>();
    private static GameObject poolAttachedObject = null;

    void OnEnable()
    {
        if (interval > 0)
            StartCoroutine(RemoveObjectCheck());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// 全部消す
    /// </summary>
    public void OnDestroy()
    {
        if (poolAttachedObject == null)
            return;

        if (poolAttachedObject.GetComponents<Pool>().Length == 1)
        {
            poolAttachedObject = null;
        }
        foreach (var obj in pooledObjectList)
        {
            Destroy(obj);
        }
        pooledObjectList.Clear();
    }

    /// <summary>
    /// 正でかつ現在と異なる値を代入したら値を更新、
    /// RemoveObjectCheckコルーチンを再スタートさせる。
    /// </summary>
    public int Interval
    {
        get
        {
            return interval;
        }
        set
        {
            if (interval != value)
            {
                interval = value;

                StopAllCoroutines();
                if (interval > 0)
                    StartCoroutine(RemoveObjectCheck());
            }
        }
    }

    /// <summary>
    /// ↓の引数がなければこれがアタッチされた帯ジェクトを親にする。
    /// </summary>
    /// <returns></returns>
    public GameObject GetInstance()
    {
        return GetInstance(transform);
    }

    /// <summary>
    /// parentの子としてプールのオブジェクトを取り出す。
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject GetInstance(Transform parent)
    {
        pooledObjectList.RemoveAll((obj) => obj == null);

        foreach (GameObject obj in pooledObjectList)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        if (pooledObjectList.Count < maxCount)
        {
            GameObject obj = (GameObject)GameObject.Instantiate(prefab);
            obj.SetActive(true);
            obj.transform.parent = parent;
            pooledObjectList.Add(obj);
            return obj;
        }

        return null;
    }

    /// <summary>
    /// interval秒ごとにRemoveObject(prepareCount)
    /// </summary>
    /// <returns></returns>
    IEnumerator RemoveObjectCheck()
    {
        while (true)
        {
            RemoveObject(prepareCount);
            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// プールサイズが期待値を超過した場合デストロイする。
    /// </summary>
    /// <param name="max">期待値</param>
    public void RemoveObject(int max)
    {
        //期待値よりも実際のプールのほうがでかい
        if (pooledObjectList.Count > max)
        {
            //プールと期待値の差
            int needRemoveCount = pooledObjectList.Count - max;
            foreach (GameObject obj in pooledObjectList.ToArray())
            {
                if (needRemoveCount == 0)
                {
                    break;
                }
                if (obj.activeSelf == false)
                {
                    pooledObjectList.Remove(obj);
                    Destroy(obj);
                    needRemoveCount--;
                }
            }
        }
    }

    /// <summary>
    /// 新しいオブジェクトにこのスクリプトのインスタンスを格納したうえでprefabを引数で初期化して生成したインスタンスを返す。
    /// </summary>
    /// <param name="obj">poolするオブジェクト</param>
    /// <returns></returns>
    public static Pool GetObjectPool(GameObject obj)
    {
        //thisがアタッチされたオブジェクトが存在するかどうか。なければ作成。
        if (poolAttachedObject == null)
        {
            poolAttachedObject = GameObject.Find("ObjectPool");
            if (poolAttachedObject == null)
            {
                poolAttachedObject = new GameObject("ObjectPool");
            }
        }

        foreach (var pool in poolAttachedObject.GetComponents<Pool>())
        {
            if (pool.prefab == obj)
            {
                return pool;
            }
        }

        foreach (var pool in FindObjectsOfType<Pool>())
        {
            if (pool.prefab == obj)
            {
                return pool;
            }
        }

        var newPool = poolAttachedObject.AddComponent<Pool>();
        newPool.prefab = obj;
        return newPool;
    }
}