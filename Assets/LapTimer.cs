using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{
    public float lapTime;
    private bool isRacing = true;
    public Text lapTimeText, lap1Text, lap2Text, lap3Text;
    public int currentLap = 0, maxLaps = 3;
    private int minutes, seconds;
    public float milliseconds;
    
    private const int STARTING_LINE_WAYPOINT_INDEX = 0;

    private AICarController car;
    private Waypoint waypoint;
    private bool doLap = true;
    private float lastLapEndTime = 0.0f;

    void Start()
    {
        car = GetComponent<AICarController>();
        waypoint = FindObjectOfType<Waypoint>();
        StartLap();
    }

    void Update()
    {
        if (currentLap > maxLaps)
        {
            Debug.Log("Car Finished");
            isRacing = false;
        }

        if (car.currentWaypoint != waypoint.transform.GetChild(STARTING_LINE_WAYPOINT_INDEX))
        {
            doLap = true;
        }

        if (car.currentWaypoint == waypoint.transform.GetChild(STARTING_LINE_WAYPOINT_INDEX) && doLap && isRacing)
        {
            EndLap();
        }

        if (isRacing)
        {
            lapTime += Time.deltaTime;
            UpdateLapTimeDisplay();
        }
    }

    private void UpdateLapTimeDisplay()
    {
        minutes = (int)lapTime / 60;
        seconds = (int)lapTime % 60;
        milliseconds = (lapTime * 100) % 100;

        lapTimeText.text = "Time: " + FormatLapTime(lapTime);
    }

    private string FormatLapTime(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;
        float msec = (time * 100) % 100;
        return string.Format("{0:00}:{1:00}.{2:00}", min, sec, msec);
    }

    public void EndLap()
    {
        float lapDuration = lapTime - lastLapEndTime;

        switch (currentLap)
        {
            case 0:
                currentLap = 1;
                break;
            case 1:
                lap1Text.text = "Lap 1: " + FormatLapTime(lapDuration);
                currentLap = 2;
                break;
            case 2:
                lap2Text.text = "Lap 2: " + FormatLapTime(lapDuration);
                currentLap = 3;
                break;
            case 3:
                lap3Text.text = "Lap 3: " + FormatLapTime(lapDuration);
                currentLap = 4;
                break;
        }

        doLap = false;
        lastLapEndTime = lapTime;
        StartLap();
    }

    public void StartLap()
    {
        isRacing = true;
    }
}
