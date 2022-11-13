using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigCube : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private bool _isCloserPlayer;
    GameObject ob;

    public void ProduceModel(PoolObjectType type)
    {
        for (int i = 0; i < 5; i++)
        {
            ob = PoolManager.Instance.GetPoolObject(type);
            ob.transform.position = new Vector3(Random.Range(transform.position.x, transform.position.x + 2f), 1.2f, Random.Range(transform.position.z - 3f, transform.position.z - 7f));
            ob.gameObject.SetActive(true);

        }

    }
}
