using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _shakeIntensity = 0.3f;

    private Vector3 _basePosition;

    private void Start() {
        _basePosition = transform.position;
    } 

    public void CameraShake(float totalShakingTime, float shakeTime){
        StartCoroutine(CameraShakeRoutine(totalShakingTime,shakeTime));
    }

    private Vector3 GetShakeDiraction(){
        return Random.insideUnitCircle.normalized;
    }

    private Vector3 GetShakeDiraction(Vector3 lastDiraction){
        float randomAngle = Random.Range(120f,240f);
        return Quaternion.Euler(0,0,randomAngle) * lastDiraction;
    }

    private IEnumerator CameraShakeRoutine(float totalShakingTime,float shakeTime){
       float timeRemaining = totalShakingTime;
       Vector3 shakeDirection =  GetShakeDiraction();
       WaitForEndOfFrame wait = new WaitForEndOfFrame();

       while(timeRemaining > 0){
            Vector3 shakeTarget = _basePosition + shakeDirection * _shakeIntensity;
            float currentTime = -shakeTime;
            while(currentTime < shakeTime){
                float progress = Mathf.Abs(currentTime) / shakeTime;

                Vector3 interpolatePosition = Vector3.Lerp(shakeTarget,_basePosition,progress);
                transform.position = interpolatePosition;

                yield return wait;
                timeRemaining -=Time.deltaTime;
                currentTime += Time.deltaTime;
            }
            shakeDirection = GetShakeDiraction(shakeDirection);
       }
       transform.position = _basePosition;
    }
}
