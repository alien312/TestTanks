using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BulletObject : MonoBehaviour
{
    public float Damage
    {
        set => m_Damage = value;
    }
    public Vector3 SpeedVector
    {
        set => m_Rigidbody.velocity = value;
    }

    Rigidbody m_Rigidbody;
    float m_Damage;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Health = -m_Damage * enemy.EnemyArmor;
        }
        if(!other.gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
    }
}
