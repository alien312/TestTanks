using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIWeaponTextController : MonoBehaviour
{
    Text m_Text;
    void Start()
    {
        EventManager.Instance.AddListener(EventType.WeaponChanged, OnWeaponChanged);
        m_Text = GetComponent<Text>();
    }

    public void OnWeaponChanged(EventType eventType, Component sender)
    {
        var pController = sender.GetComponent<PlayerController>();
        m_Text.text = pController.CurrentWeaponObject.WeaponName;
    }
}
