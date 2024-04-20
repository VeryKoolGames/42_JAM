using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera VCam;
    public float ShakeIntensity = 1f;
    public float ShakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    void Awake(){
        VCam = GetComponent<CinemachineVirtualCamera>();
    }

    void Start(){
        StopShake();
    }


    public void ShakeCamera(){
        CinemachineBasicMultiChannelPerlin _cbmcp = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;
        timer = ShakeTime;
    }

    public void StopShake(){
        CinemachineBasicMultiChannelPerlin _cbmcp = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
        timer = 0;
    }

        void Update()
    {

        if(timer > 0){
            timer -= Time.deltaTime;
            if(timer <= 0){
                StopShake();
            }
        }
    }


}
