using UnityEngine;

public class StarsController : MonoBehaviour
{
    [SerializeField] private int _myStar=0;

    [SerializeField] private float _timeAppearStar;
    [SerializeField] private GameObject[] _stars;

    private Vector3 _maxScaleStar;
    private float _timer;
    private int _currentStar;

    public void SetStar(int star){
        Debug.Log(star);
       _myStar = star;
    }

    private void Start() {
        _stars = new GameObject[3];
        for(int i = 0; i < transform.childCount; i++){
            _stars[i] = transform.GetChild(i).GetChild(0).gameObject;
        }

        _maxScaleStar = new Vector3(1,1,1);
    }

    private void Update() {
        if(_myStar == _currentStar ) this.enabled = false;

        float progress = _timer/_timeAppearStar;
        Vector3 starScale =Vector3.Lerp(Vector3.zero,_maxScaleStar,progress);
        _stars[_currentStar].transform.localScale = starScale;

        if(_stars[_currentStar].transform.localScale == _maxScaleStar){
            _currentStar++;
            _timer = 0;
        }

        _timer +=Time.deltaTime;
    }
}
