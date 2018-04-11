using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
    
    static float timer = 0.0f;
    public Text text_box;
    public bool isTimerStopped;

    public void ResetTimer() {
        timer = 0.0f;
        isTimerStopped = false;
    }

    public void FlipTimerPause() {
        isTimerStopped = !isTimerStopped;
        Canvas pauseCanvas = GameObject.Find("Pause Canvas").GetComponent<Canvas>();

        if(isTimerStopped) { 
            pauseCanvas.enabled = true;
        } else {
            pauseCanvas.enabled = false;
        }
    }

    public void StopTimerGameOver() {
        isTimerStopped = !isTimerStopped;
    }

    void Update() {
       if (!isTimerStopped) {
            timer += Time.deltaTime;
            text_box.text = string.Format("{0}:{1:00}", (int)timer / 60, (int)timer % 60);
            //text_box.text = timer.ToString("0:00");
        }
    }
}
