using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float MaxHealth;
    [SerializeField] float MovmentSpeed;
    [SerializeField] float MaxDamage;
    [SerializeField] [Range(0, 1)] float Armor;

    public float Health
    {
        get { return m_Health; }
        set
        {
            if (value < 0)
                m_Animator.SetTrigger(GetHit);
            m_Health += value;
            if(m_Health <= 0)
                EventManager.Instance.PostNotification(EventType.EnemyDied, this);
        }
    }
    public float EnemyArmor => 1 - Armor;
    float m_Health;
    const float CooldownTime = 3f;
    float m_CurrentCooldownTime = 0f;

    NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;
    
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int GetHit = Animator.StringToHash("Get_Hit");

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        m_NavMeshAgent.speed = MovmentSpeed;
        m_Health = MaxHealth;
    }
    void Start()
    {
        EventManager.Instance.AddListener(EventType.PlayerMoved, OnEvent);
        m_NavMeshAgent.SetDestination(Player.Instance.transform.position);
        m_Animator.SetBool(IsRunning, true);
    }

    void FixedUpdate()
    {
        if(m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
        {
            m_Animator.SetBool(IsRunning, false);
        }
    }
    public void OnEvent(EventType eventType, Component sender)
    {
        switch (eventType)
        {
            case EventType.PlayerMoved:
                OnPlayerMoved(sender.transform.position);
                break;
        }
    }
    void OnPlayerMoved(Vector3 playerPos)
    {
        if (isActiveAndEnabled)
        {
            m_NavMeshAgent.SetDestination(playerPos);
            m_Animator.SetBool(IsRunning, true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            player.Health = - MaxDamage * player.Armor;

            m_Animator.SetTrigger(IsAttacking);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (m_CurrentCooldownTime >= CooldownTime)
            {
                var player = other.gameObject.GetComponent<Player>();
                player.Health = -MaxDamage * player.Armor;

                m_Animator.SetTrigger(IsAttacking);
                m_CurrentCooldownTime = 0f;
            }
            else
                m_CurrentCooldownTime += Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_CurrentCooldownTime += Time.deltaTime;
        }
    }

    public void OnDeath()
    {
        m_Health = MaxHealth;
    }
}
