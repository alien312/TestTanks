using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData", order = 51)]
public class WeaponData : ScriptableObject
{
    [SerializeField] string WeaponName;
    [SerializeField] float WeaponDamage;
    [SerializeField] float WeaponAttackSpeed;
    
    public string Name => WeaponName;
    public float Damage => WeaponDamage;
    public float AttackSpeed => WeaponAttackSpeed;
}