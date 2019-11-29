using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    [SerializeField] WeaponData WeaponData;
    [SerializeField] Pool BulletPool;


    public float WeaponDamage => WeaponData.Damage;
    public string WeaponName => WeaponData.Name;
    public float WeaponAttackSpeed => WeaponData.AttackSpeed;
    public string BulletPoolTag => BulletPool.tag;
    private void Awake()
    {
        BulletPool.OnObjectLoaded();
    }
}
