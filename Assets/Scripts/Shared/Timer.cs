using UnityEngine;
using TMPro;
using Mirror;
public class Timer : NetworkBehaviour {
    [SerializeField]
    private float timerDuration; //Duration of the timer in seconds

    [SerializeField]
    private bool countDown = true;

    [SyncVar]
    private float timer;
/*    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;*/

    //Use this for a single text object
    //[SerializeField]
    //private TextMeshProUGUI timerText;

    private float flashTimer;
    [SerializeField]
    private float flashDuration = 1f; //The full length of the flash

    private void Start() {
        //ResetTimer();
    }

    public float GetTime()
    {
        return timer;
    }

    public void SetDuration(float duration)
    {
        timerDuration = duration*60;
    }

    public void ResetTimer() {
        if (countDown) {
            timer = timerDuration;
        } else {
            timer = 0;
        }
        //SetTextDisplay(true);
    }

    void Update() {
        if (!isServer)return;
        if (countDown && timer > 0) {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        } else if (!countDown && timer < timerDuration) {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        } else {
            FlashTimer();
        }
    }

    private void UpdateTimerDisplay(float time) {
        if (time < 0) {
            time = 0;
        }

        if (time > 3660) {
            Debug.LogError("Timer cannot display values above 3660 seconds");
            ErrorDisplay();
            return;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{01:00}", minutes, seconds);
/*        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();*/

        //Use this for a single text object
        //timerText.text = currentTime.ToString();
    }

    private void ErrorDisplay() {
/*        firstMinute.text = "8";
        secondMinute.text = "8";
        firstSecond.text = "8";
        secondSecond.text = "8";*/


        //Use this for a single text object
        //timerText.text = "ERROR";
    }

    private void FlashTimer() {
        if(countDown && timer != 0) {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if(!countDown && timer != timerDuration) {
            timer = timerDuration;
            UpdateTimerDisplay(timer);
        }

        if(flashTimer <= 0) {
            flashTimer = flashDuration;
        } else if (flashTimer <= flashDuration / 2) {
            flashTimer -= Time.deltaTime;
            //SetTextDisplay(true);
        } else {
            flashTimer -= Time.deltaTime;
            //SetTextDisplay(false);
        }
    }

    public bool GameOn()
    {
        return timer != 0;
    }

/*    private void SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        separator.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;

        //Use this for a single text object
        //timerText.enabled = enabled;
    }*/
}
