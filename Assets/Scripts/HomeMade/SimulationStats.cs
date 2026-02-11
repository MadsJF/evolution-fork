using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Keiwando.Evolution;

public class SimulationStats : MonoBehaviour {

    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] TextMeshProUGUI timeScaleText;
    [SerializeField] Slider timeScaleSlider;

    private Evolution evolution;
    private CameraFollowController cameraFollowController;
    private float simulationStartTime;
    private Creature currentTrackedCreature;
    private Vector3 trackedCreatureStartPosition;

    void Start() {
        evolution = FindAnyObjectByType<Evolution>();
        cameraFollowController = FindAnyObjectByType<CameraFollowController>();

        if (evolution != null) {
            simulationStartTime = Time.time;
            evolution.NewBatchDidBegin += OnNewBatchStarted;
        }
    }

    private void OnNewBatchStarted() {
        simulationStartTime = Time.time;
        UpdateTrackedCreature();
    }

    void Update() {
        float newTimeScale = timeScaleSlider.value;
        if (Time.timeScale != newTimeScale) {
            Time.timeScale = newTimeScale;
            timeScaleText.text = "TIMESCALE: " + newTimeScale.ToString("F1") + "X";
        }

        // Update the three variables
        UpdateTrackedCreature();
        float averageSpeed = CalculateAverageSpeed();
        float horizontalDistance = CalculateHorizontalDistance();
        float verticalDistance = CalculateVerticalDistance();

        statsText.text = "AVERAGE SPEED:" + averageSpeed.ToString("F1") + " m/s ("+(averageSpeed*3.6).ToString("F1")+" km/h) \n" +
            "HORIZ. DISTANCE FROM START: " + horizontalDistance.ToString("F1") + " m\n" +
            "VERT. DISTANCE FROM START: " + verticalDistance.ToString("F1") + " m\n";
    }

    private void UpdateTrackedCreature() {
        if (cameraFollowController == null) return;

        Creature trackedCreature = cameraFollowController.GetFocusedCreature();

        if (trackedCreature != currentTrackedCreature) {
            currentTrackedCreature = trackedCreature;
            if (currentTrackedCreature != null) {
                trackedCreatureStartPosition = new Vector3(
                    currentTrackedCreature.GetXPosition(),
                    currentTrackedCreature.GetYPosition()
                );
                simulationStartTime = Time.time;
            }
        }
    }

    private float CalculateAverageSpeed() {
        if (currentTrackedCreature == null || !currentTrackedCreature.Alive) {
            return 0;
        }

        float elapsedTime = Time.time - simulationStartTime;
        float horizontalDist = currentTrackedCreature.GetXPosition() - trackedCreatureStartPosition.x;
        float verticalDist = currentTrackedCreature.GetYPosition() - trackedCreatureStartPosition.y;

        float totalDistance = Mathf.Sqrt(
            Mathf.Pow(horizontalDist, 2) +
            Mathf.Pow(verticalDist, 2)
        );

        float avgSpeed = elapsedTime > 0 ? totalDistance / elapsedTime : 0;
        return avgSpeed / 5f; // Apply gravity scaling
    }

    private float CalculateHorizontalDistance() {
        if (currentTrackedCreature == null || !currentTrackedCreature.Alive) {
            return 0;
        }

        float horizontalDist = currentTrackedCreature.GetXPosition() - trackedCreatureStartPosition.x;
        return horizontalDist / 5f; // Apply gravity scaling
    }

    private float CalculateVerticalDistance() {
        if (currentTrackedCreature == null || !currentTrackedCreature.Alive) {
            return 0;
        }

        float verticalDist = currentTrackedCreature.GetYPosition() - trackedCreatureStartPosition.y;
        return verticalDist / 5f; // Apply gravity scaling
    }

    private void OnDestroy() {
        if (evolution != null) {
            evolution.NewBatchDidBegin -= OnNewBatchStarted;
        }
    }
}