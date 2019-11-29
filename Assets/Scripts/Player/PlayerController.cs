using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Controls Controls;
    
    //-----------------------------------------------------------------------------------
    public float MovementSpeed
    {
        set => m_MovementSpeed = value;
    }
    public WeaponObject[] Weapons
    {
        set => m_Weapons = value;
    }
    
    public WeaponObject CurrentWeaponObject => m_Weapons[m_CurrentWeaponId];
    
    //-----------------------------------------------------------------------------------
    WeaponObject[] m_Weapons;
    float CurrentWeaponDamage => CurrentWeaponObject.WeaponDamage;
    float CurrentWeaponAttackSpeed => CurrentWeaponObject.WeaponAttackSpeed;

    float m_MovementSpeed;
    Vector3 m_MovementVector;
    float m_Angle;

    int m_CurrentWeaponId = 0;
    int m_WeaponsCount;
    Rigidbody m_Rigidbody;
    
    const float RotationSpeed = 55f;
    //-----------------------------------------------------------------------------------

    #region Методы

    void Awake()
    { 
        //Ассоциируем обработчики ввода
        Controls.Player.Movment.performed += context => Move(context.ReadValue<float>());
        Controls.Player.Movment.cancelled += context => Move(0);

        Controls.Player.Rotation.performed += context => Rotate(context.ReadValue<float>());
        Controls.Player.Rotation.cancelled += context => Rotate(0);

        Controls.Player.Shooting.performed += context => Shoot();

        Controls.Player.WeaponChange.performed += context => SwitchWeapon(context.ReadValue<float>());

        m_Rigidbody = GetComponent<Rigidbody>();
        m_WeaponsCount = m_Weapons.Length;
    }
    
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, m_Angle * Time.deltaTime * RotationSpeed);

        m_Rigidbody.MovePosition(transform.position + m_MovementVector * Time.deltaTime * m_MovementSpeed);
    }

    #region Обработчики ввода

    #region Движение и вращение
    void Move(float axis)
    {
        m_MovementVector = transform.forward * axis;
        EventManager.Instance.PostNotification(EventType.PlayerMoved, this);
    }
    void Rotate(float angle)
    {
        m_Angle = angle;
    }
    #endregion

    #region Смена оружия
    private void SwitchWeapon(float value)
    {
        m_Weapons[m_CurrentWeaponId].gameObject.SetActive(false);
        m_CurrentWeaponId = Mathf.Abs(m_CurrentWeaponId + (int)value) % m_WeaponsCount;
        m_Weapons[m_CurrentWeaponId].gameObject.SetActive(true);
        
        EventManager.Instance.PostNotification(EventType.WeaponChanged, this);
    }
    #endregion

    #region Выстрел
    private void Shoot()
    {
        var spawnObj = ObjectPooler.Instance.SpawnFromPool(CurrentWeaponObject.BulletPoolTag, CurrentWeaponObject.transform.position);
        if (!ReferenceEquals(spawnObj, null))
        {
            var bullet = spawnObj.GetComponent<BulletObject>();
            bullet.Damage = CurrentWeaponDamage;
            bullet.SpeedVector = Player.Instance.transform.forward * CurrentWeaponAttackSpeed;
        }
    }
    #endregion

    #endregion

    #endregion
    
    private void OnEnable()
    {
        Controls.Player.Movment.Enable();
        Controls.Player.Rotation.Enable();
        Controls.Player.WeaponChange.Enable();
        Controls.Player.Shooting.Enable();
    }
    private void OnDisable()
    {
        Controls.Player.Movment.Disable();
        Controls.Player.Rotation.Disable();
        Controls.Player.WeaponChange.Disable();
        Controls.Player.Shooting.Disable();
    }
}
