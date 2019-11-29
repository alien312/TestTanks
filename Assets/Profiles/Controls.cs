// GENERATED AUTOMATICALLY FROM 'Assets/Profiles/Controls.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class Controls : InputActionAssetReference
{
    public Controls()
    {
    }
    public Controls(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Movment = m_Player.GetAction("Movment");
        m_Player_Rotation = m_Player.GetAction("Rotation");
        m_Player_Shooting = m_Player.GetAction("Shooting");
        m_Player_WeaponChange = m_Player.GetAction("WeaponChange");
        m_Player_UIRestart = m_Player.GetAction("UI-Restart");
        m_Player_UIExit = m_Player.GetAction("UI-Exit");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Player = null;
        m_Player_Movment = null;
        m_Player_Rotation = null;
        m_Player_Shooting = null;
        m_Player_WeaponChange = null;
        m_Player_UIRestart = null;
        m_Player_UIExit = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Player
    private InputActionMap m_Player;
    private InputAction m_Player_Movment;
    private InputAction m_Player_Rotation;
    private InputAction m_Player_Shooting;
    private InputAction m_Player_WeaponChange;
    private InputAction m_Player_UIRestart;
    private InputAction m_Player_UIExit;
    public struct PlayerActions
    {
        private Controls m_Wrapper;
        public PlayerActions(Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movment { get { return m_Wrapper.m_Player_Movment; } }
        public InputAction @Rotation { get { return m_Wrapper.m_Player_Rotation; } }
        public InputAction @Shooting { get { return m_Wrapper.m_Player_Shooting; } }
        public InputAction @WeaponChange { get { return m_Wrapper.m_Player_WeaponChange; } }
        public InputAction @UIRestart { get { return m_Wrapper.m_Player_UIRestart; } }
        public InputAction @UIExit { get { return m_Wrapper.m_Player_UIExit; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
    }
    public PlayerActions @Player
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerActions(this);
        }
    }
    private int m_keyboardInputSchemeIndex = -1;
    public InputControlScheme keyboardInputScheme
    {
        get

        {
            if (m_keyboardInputSchemeIndex == -1) m_keyboardInputSchemeIndex = asset.GetControlSchemeIndex("keyboardInput");
            return asset.controlSchemes[m_keyboardInputSchemeIndex];
        }
    }
}
