using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    Dictionary<string, Queue<GameObject>> m_PoolDictionary;

    public static ObjectPooler Instance { get; private set; } = null;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            DestroyImmediate(this);

        m_PoolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public void AddNewPool(Pool pool)
    {
        var objectPool = new Queue<GameObject>();
        for (var i = 0; i < pool.size; i++)
        {
            var go = Instantiate(pool.prefab);
            go.SetActive(false);
            objectPool.Enqueue(go);
        }
        m_PoolDictionary.Add(pool.tag, objectPool);
    }

    public void OnSceenChanged()
    {
        m_PoolDictionary.Clear();
    }
    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (m_PoolDictionary.ContainsKey(tag))
        {
            var obj = m_PoolDictionary[tag].Dequeue();
            obj.transform.position = position;
            obj.SetActive(true);
            
            //var pooledObj = obj.GetComponent<IPoolingObject>();
            //pooledObj?.onObjectSpawn();

            m_PoolDictionary[tag].Enqueue(obj);
            return obj;
        }
        else return null;
    }
}
