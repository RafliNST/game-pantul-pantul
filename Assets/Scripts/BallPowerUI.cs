using UnityEngine;
using UnityEngine.UI;

public class BallPowerUI : MonoBehaviour
{
    [SerializeField] Slider powerSlider;
    [SerializeField] BallControl ballControl;
    [SerializeField] float sliderChangeSpeed, resetDelay;
    [SerializeField] Gradient sliderChangerColor;
    [SerializeField] Image _image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballControl.speedForceChange.AddListener(ChangeSliderVal);
        ballControl.ballReleased.AddListener(ResetSlider);
        powerSlider.maxValue = ballControl.GetMaxSpeed();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSliderVal(float val)
    {
        powerSlider.value = Mathf.MoveTowards(powerSlider.value, val, sliderChangeSpeed * Time.deltaTime);
        _image.color = sliderChangerColor.Evaluate(powerSlider.normalizedValue);
    }

    void ResetSlider()
    {
        StartCoroutine(ReduceSliderVal(resetDelay));
    }

    System.Collections.IEnumerator ReduceSliderVal(float delay)
    {
        yield return new WaitForSeconds(delay);
        powerSlider.value = 0;
    }

    public void SetSliderMaxVal (float val)
    {
        powerSlider.maxValue = val;
    }    
}
