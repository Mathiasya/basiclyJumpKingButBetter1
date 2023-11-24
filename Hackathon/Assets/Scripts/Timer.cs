using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    [SerializeField] public bool isTimerRunning = false;
    public GameObject finishLine;
    


    void Start()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            string minutes = ((int)elapsedTime / 60).ToString("00");
            string seconds = (elapsedTime % 60).ToString("00.00");
            timerText.text = minutes + ":" + seconds;
        }

        

    }

    public void CurrentTimerState()
    {
        isTimerRunning = false;
    }
    


}
