using UnityEngine;

[System.Serializable]
public struct Pool 
{
    public string tag;
    public GameObject prefab;
    public int size;

    /// <summary>
    /// Метод должен вызываться для добавления конкретного пула в глобальный список пулов.
    /// </summary>
    public void OnObjectLoaded()
    {
        ObjectPooler.Instance.AddNewPool(this);
    }
}

