using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] TextMeshProUGUI timeScaleText;
    [SerializeField] Slider timeScaleSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTimeChange() {
        float newTimeScale = timeScaleSlider.value;
        Time.timeScale = newTimeScale;
        timeScaleText.text = "TIMESCALE: " + newTimeScale.ToString("F1") + "X";
    }
}
