using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Cube
}
[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}


public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] List<PoolInfo> listOfPool;

    private Vector3 _defaultPos = new Vector3(-100, -100, -100);

    private void Start()
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            FillPool(listOfPool[i]);
        }
    }

    void FillPool(PoolInfo info)
    {
        for (int i = 0; i < info.amount; i++)
        {
            GameObject obInstance = null;
            obInstance = Instantiate(info.prefab, info.container.transform);
            obInstance.gameObject.SetActive(false);
            obInstance.transform.position = _defaultPos;
            info.pool.Add(obInstance);
        }
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo _selected = GetPoolByType(type);
        List<GameObject> _pool = _selected.pool;

        GameObject obInstance = null;
        if (_pool.Count > 0)
        {
            obInstance = _pool[_pool.Count - 1];
            _pool.Remove(obInstance);
        }
        else
        {
           obInstance = Instantiate(_selected.prefab, _selected.container.transform);
        }
        return obInstance;
    }

    public void CoolObject(GameObject ob, PoolObjectType type)
    {
        ob.SetActive(false);
        ob.transform.position = _defaultPos;

        PoolInfo _selected = GetPoolByType(type);
        List<GameObject> _pool = _selected.pool;

        if (!_pool.Contains(ob))
        {
            _pool.Add(ob);
        }
    }

    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            if (type == listOfPool[i].type)
            {
                return listOfPool[i];
            }
        }
        return null;
    }
}
