using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Image = UnityEngine.UI.Image;

public class CameraFade : MonoBehaviour
{
    private static CameraFade _cameraInstance;
    public static CameraFade CameraInstance
    {
        get
        { 
            return _cameraInstance;
        }
        
    }

    [SerializeField] private Image blackFade; 
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float shakeTimer;

    private void Awake()
    {
        if (_cameraInstance != null && _cameraInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _cameraInstance = this;
        }
        
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        blackFade.canvasRenderer.SetAlpha(0f);
    }

    public void FadeOut(float duration, bool ignoreTimeScale)
    {
        Color fixedColor = blackFade.color;
        fixedColor.a = 1;
        blackFade.color = fixedColor;
        
        blackFade.CrossFadeAlpha(0f,0f,true);
        blackFade.CrossFadeAlpha(1f, duration, ignoreTimeScale);
    }

    public void FadeIn(float duration, bool ignoreTimeScale)
    {
        Color fixedColor = blackFade.color;
        fixedColor.a = 1;
        blackFade.color = fixedColor;
        
        blackFade.CrossFadeAlpha(1f,0f,true);
        blackFade.CrossFadeAlpha(0f, duration, ignoreTimeScale);
    }
    
}
