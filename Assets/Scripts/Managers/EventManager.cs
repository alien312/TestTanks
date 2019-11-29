using UnityEngine;
using System.Collections.Generic;

public enum EventType
{
    PlayerMoved,
    HealthChanged,
    WeaponChanged,
    PlayerDied,
    EnemyDied
}

public delegate void OnEvent(EventType eventType, Component sender);

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; } = null;
    Dictionary<EventType, List<OnEvent>> m_Listeners = new Dictionary<EventType, List<OnEvent>>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }
    
    public void AddListener(EventType eventType, OnEvent listener)
    {
        List<OnEvent> ListenList = null;

        if (m_Listeners.TryGetValue(eventType, out ListenList))
        {
            ListenList.Add(listener);
            return;
        }

        ListenList = new List<OnEvent>();
        ListenList.Add(listener);
        m_Listeners.Add(eventType, ListenList);
    }
    
    public void PostNotification(EventType eventType, Component sender)
    {
        List<OnEvent> ListenList = null;

        if (!m_Listeners.TryGetValue(eventType, out ListenList))
            return;

        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
                ListenList[i](eventType, sender);
        }
    }
}
