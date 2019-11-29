using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIHealthController : MonoBehaviour
{
    [SerializeField] Text HealthText;
    
    RectTransform m_HealthTransform;
    private float cachedY;
    private float maxXValue;
    private float minXValue;

    private float currentHealth;
    private float maxHealth = 0;
    private float minHealth = 0;
    private Image visualHealth; 
    
    const byte ByteMax = Byte.MaxValue;
    const byte ByteMin = Byte.MinValue;
    void Start()
    {
        EventManager.Instance.AddListener(EventType.HealthChanged, OnHealthChanged);
        
        visualHealth = GetComponent<Image>();
        m_HealthTransform = GetComponent<RectTransform>();
        
        cachedY = m_HealthTransform.localPosition.y;
        maxXValue = m_HealthTransform.localPosition.x;
        minXValue = maxXValue - m_HealthTransform.rect.width;
        currentHealth = maxHealth;
    }

    public void OnHealthChanged(EventType eventType, Component sender)
    {
        currentHealth = ((Player) sender).Health;
        if (maxHealth == 0)
        {
            maxHealth = currentHealth;
        }
        else
        {
            HealthText.text = Math.Round(currentHealth, 1).ToString();
            var mappedX = MappedValue(currentHealth, minHealth, maxHealth, minXValue, maxXValue);
            m_HealthTransform.localPosition = new Vector3(mappedX, cachedY);
            if (currentHealth >= maxHealth / 2)
            {
                visualHealth.color = new Color32((byte) MappedValue(currentHealth, maxHealth / 2, maxHealth, ByteMax, ByteMin),
                    ByteMax, ByteMin, ByteMax);
            }
            else
            {
                visualHealth.color = new Color32(ByteMax, (byte) MappedValue(currentHealth, 0, maxHealth/2, ByteMin, ByteMax),
                    ByteMin, ByteMax);
            }
        }
    }

    float MappedValue(float value, float minValue, float maxValue, float minMapValue, float maxMapValue)
    {
        return (value - minValue) * (maxMapValue - minMapValue) / (maxValue - minValue) + minMapValue;
    }
}
