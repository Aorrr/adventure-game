using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShakeController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCameraIdle;
    public CinemachineVirtualCamera virtualCameraRun;
    private CinemachineBasicMultiChannelPerlin virtualCameraIdleNoise;
    private CinemachineBasicMultiChannelPerlin virtualCameraRunNoise;

    // Start is called before the first frame update
    void Start()
    {
        if (virtualCameraIdle != null)
            virtualCameraIdleNoise = virtualCameraIdle.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCameraRun != null)
            virtualCameraRunNoise = virtualCameraRun.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator ShakeIdle(float shakeDuration, float shakeAmplitude, float shakeFrequency)
    {
        if (virtualCameraIdle != null || virtualCameraIdleNoise != null)
        {
            virtualCameraIdleNoise.m_AmplitudeGain = shakeAmplitude;
            virtualCameraIdleNoise.m_FrequencyGain = shakeFrequency;
            //Debug.Log("yes");
            yield return new WaitForSeconds(shakeDuration);
        }
        virtualCameraIdleNoise.m_AmplitudeGain = 0f;
        //Debug.Log("stop");
    }

    public IEnumerator ShakeRun(float shakeDuration, float shakeAmplitude, float shakeFrequency)
    {
        if (virtualCameraRun != null || virtualCameraRunNoise != null)
        {
            virtualCameraRunNoise.m_AmplitudeGain = shakeAmplitude;
            virtualCameraRunNoise.m_FrequencyGain = shakeFrequency;
            //Debug.Log("yes");
            yield return new WaitForSeconds(shakeDuration);
        }
        virtualCameraRunNoise.m_AmplitudeGain = 0f;
        //Debug.Log("stop");
    }
}
