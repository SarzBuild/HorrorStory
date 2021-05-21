using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private CameraFade _cameraFade;

    void Start()
    {
        _cameraFade = CameraFade.CameraInstance;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Water"))
        {
            SendMessageUpwards("HandleDamage", 10,  SendMessageOptions.DontRequireReceiver);
            _cameraFade.FadeOut(0.2f,false);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            SendMessageUpwards("HandleDamage", 10,  SendMessageOptions.DontRequireReceiver);
            _cameraFade.FadeOut(0.2f,false);
        }
    }
}
