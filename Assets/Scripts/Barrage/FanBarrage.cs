using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IShotable/Create FanBarrage", fileName = "FanBarrage")]
[System.Serializable]
public class FanBarrage : BaseBarrage
{
    [SerializeField] int fan_way = 1;
    [SerializeField] float fan_space;//*PI
    [SerializeField] int fan_fold = 1;
    [SerializeField] float fan_delay;

    public override GameObject[] Shot()
    {
        float _speed = speed;
        List<GameObject> gameObjects = new List<GameObject>();
        shotAngle = ((fan_way - 1) * fan_space) / 2;
        for (int i = 0; i < fan_way; i++)
        {
            for (int j = 0; j < fan_fold; j++)
            {
                gameObjects.AddRange(base.Shot());
                speed -= fan_delay;
            }
            speed = _speed;
            shotAngle -= fan_space;
        }
        return gameObjects.ToArray();
    }
}
