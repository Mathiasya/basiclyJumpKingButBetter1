using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stoptimer : MonoBehaviour


{

    public Timer timer;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == CompareTag("FinishLine"))
        {
            
            Debug.Log("Player reached the finish line!");
            // Optionally, you can call a method to handle stopping the timer or saving the completion time.
            timer.CurrentTimerState();
        }
    }
}
