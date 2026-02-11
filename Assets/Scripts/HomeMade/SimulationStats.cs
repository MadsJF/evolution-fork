using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Keiwando.Evolution;


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
        float newTimeScale = timeScaleSlider.value;
        if (Time.timeScale != newTimeScale) {
            Time.timeScale = newTimeScale;
            timeScaleText.text = "TIMESCALE: " + newTimeScale.ToString("F1") + "X";
        }
        float averageSpeed = 0;
        float horizontalDistance = 0;
        float verticalDistance = 0; 
        statsText.text = "AVERAGE SPEED:"+averageSpeed+"+ m/s\n" +
            "HORIZ. DISTANCE FROM START: "+horizontalDistance + " m\n" +
            "VERT. DISTANCE FROM START: "+verticalDistance+" m\n";
        
    }

}
