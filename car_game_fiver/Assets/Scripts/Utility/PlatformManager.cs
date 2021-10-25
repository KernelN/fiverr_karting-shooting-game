using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public enum Platforms { Computer, Mobile }
    public Platforms currentPlatform { get; private set; }
    [SerializeField] GameObject mobileInputsUI;
    [SerializeField] KartGame.KartSystems.MobileInput mobileInputs;
    [SerializeField] KartGame.KartSystems.KeyboardInput keyboardInputs;
    [SerializeField] KartGame.KartSystems.ArcadeKart kart;
    

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        currentPlatform = Platforms.Mobile;
#else
        currentPlatform = Platforms.Computer;
#endif         
    }

    private void Start()
    {
        switch (currentPlatform)
        {
            case Platforms.Computer:
                DeactivateMobile();
                ActivateComputer();
                break;
            case Platforms.Mobile:
                DeactivateComputer();
                ActivateMobile();
                break;
            default:
                break;
        }
    }

    void DeactivateMobile()
    {
        mobileInputs.enabled = false;
        Destroy(mobileInputs);
        mobileInputsUI.SetActive(false);
    }
    void DeactivateComputer()
    {
        keyboardInputs.enabled = false;
        Destroy(keyboardInputs);
    }
    void ActivateMobile()
    {
        kart.m_Input = mobileInputs;
    }
    void ActivateComputer()
    {
        kart.m_Input = keyboardInputs;
    }
}
