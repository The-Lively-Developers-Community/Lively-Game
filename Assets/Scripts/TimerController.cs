using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    private bool timing = false;
    private float time = 0f;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        //timerText = GetComponent<TextMeshProUGUI>();
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timing)
        {
            time += Time.deltaTime;
            UpdateTimerText();
        }
    }

    void StartTimer()
    {
        timing = true;
    }

    void StopTimer()
    {
        timing = false;
    }

    void UpdateTimerText()
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        int milliseconds = (int)((time % 1f) * 1000f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            StopTimer();
        }
    }
}
