using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] int MaxEnemyCountOnSceen;
    [SerializeField] List<Pool> SpawanPools;
    [SerializeField] List<Transform> SpawnPoints;
    [SerializeField] GameObject Particles;
    [SerializeField] [Range(1, 10)] int InitialEnemyAmount;
    
    //-----------------------------------------------------------------------------------
    int m_CurrentEnemyCount = 0;
    ObjectPooler m_ObjectPooler;
    //-----------------------------------------------------------------------------------
    void Start()
    {
        EventManager.Instance.AddListener(EventType.PlayerDied, onEvent);
        EventManager.Instance.AddListener(EventType.EnemyDied, onEvent);
        
        //Инициализируем пулы для генерации мобов
        m_ObjectPooler = ObjectPooler.Instance;
        foreach (var spawanPool in SpawanPools)
            spawanPool.OnObjectLoaded();
        for (int i = 0; i < InitialEnemyAmount; i++)
            SpawnEnemy();
    }
    public void onEvent(EventType eventType, Component sender)
    {
        switch (eventType)
        {
            case EventType.PlayerDied:
                onPlayerDied();
                break;
            case EventType.EnemyDied:
                OnEnemyDied(sender.GetComponent<Enemy>());
                break;
        }
    }

    void onPlayerDied()
    {
        ObjectPooler.Instance.OnSceenChanged();
        Player.Instance.OnSceneChanged();
        SceneManager.LoadScene(1);
    }
    void OnEnemyDied(Enemy enemy)
    {
        enemy.OnDeath();
        enemy.gameObject.SetActive(false);
        Instantiate(Particles, enemy.transform.position, Quaternion.identity);
        m_CurrentEnemyCount--;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (m_CurrentEnemyCount <= MaxEnemyCountOnSceen)
        {
            m_ObjectPooler.SpawnFromPool(SpawanPools[Random.Range(0, SpawanPools.Count)].tag,
                SpawnPoints[Random.Range(0, SpawnPoints.Count)].position);
            m_CurrentEnemyCount++;
        }
    }
}
