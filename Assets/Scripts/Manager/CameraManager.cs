using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _shakeIntensity = 0.3f;

    private Vector3 _basePosition;

    private void Start() {
        _basePosition = transform.position;
    } 

    public void CameraShake(float totalShakingTime, float shakeTime,bool isFade){
        StartCoroutine(CameraShakeRoutine(totalShakingTime,shakeTime,isFade));
    }

    private Vector3 GetShakeDirection(){
        return Random.insideUnitCircle.normalized;
    }

    private Vector3 GetShakeDirection(Vector3 lastDirection){
        float randomAngle = Random.Range(120f,240f);
        return Quaternion.Euler(0,0,randomAngle) * lastDirection;
    }

    private IEnumerator CameraShakeRoutine(float totalShakingTime,float shakeTime,bool isFade){
       float timeRemaining = totalShakingTime;
       Vector3 shakeDirection =  GetShakeDirection();
       WaitForEndOfFrame wait = new WaitForEndOfFrame();

       while(timeRemaining > 0){
            float magnitude = isFade ? Mathf.Lerp(0,_shakeIntensity,timeRemaining/shakeTime) :_shakeIntensity;
            Vector3 shakeTarget = _basePosition + shakeDirection * magnitude;
            float currentTime = -shakeTime;
            while(currentTime < shakeTime){
                float progress = Mathf.Abs(currentTime) / shakeTime;

                Vector3 interpolatePosition = Vector3.Lerp(shakeTarget,_basePosition,progress);
                transform.position = interpolatePosition;

                yield return wait;
                timeRemaining -=Time.deltaTime;
                currentTime += Time.deltaTime;
            }
            shakeDirection = GetShakeDirection(shakeDirection);
       }
       transform.position = _basePosition;
    }
}
