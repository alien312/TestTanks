using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] WeaponObject[] Weapons;
    [Header("Tank Parameters")]
    [SerializeField] float TankMaxHealth;
    [SerializeField] [Range(0, 1)] float TankArmor;
    [SerializeField] float TankMovementSpeed;
    
    //-----------------------------------------------------------------------------------
    public float Health
    {
        get => m_Health;
        set
        {
            m_Health += value;
            EventManager.Instance.PostNotification(EventType.HealthChanged, this);
            if(m_Health <= 0)
                EventManager.Instance.PostNotification(EventType.PlayerDied, this);
        }
    }
    public float Armor => 1-TankArmor;
    public static Player Instance { get; private set; } = null;
    
    //-----------------------------------------------------------------------------------
    PlayerController m_MovementController;
    float m_Health;
    
    //-----------------------------------------------------------------------------------
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            DestroyImmediate(this);
        m_Health = TankMaxHealth;

        m_MovementController = GetComponent<PlayerController>();
        m_MovementController.MovementSpeed = TankMovementSpeed;
        m_MovementController.Weapons = Weapons;
    }

    void Start()
    {
        EventManager.Instance.PostNotification(EventType.HealthChanged, this);
    }

    public void OnSceneChanged()
    {
        m_Health = TankMaxHealth;
    }
}
