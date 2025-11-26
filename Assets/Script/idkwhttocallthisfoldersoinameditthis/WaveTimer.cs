using UnityEngine;
using TMPro;
using System.Collections;

public class WaveTimer : MonoBehaviour
{
    public TextMeshProUGUI waveTimer;
    public float timer;
    public EnemyWave waveScr;
    public bool isTimerOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveScr = GetComponent<EnemyWave>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerOn)
        {
            //disable text
            waveTimer.text = "";
        }
        else if (isTimerOn)
        {
            waveTimer.text = timer.ToString("F0");
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            isTimerOn = true;
        }
        else if (timer < 0)
        {
            timer = 0;
        }

        if (timer == 0)
        {
            isTimerOn = false;
        }

    }



    public IEnumerator Timer()
    {
        isTimerOn = true;
        timer = 3f;
        yield return new WaitUntil(() => isTimerOn == false);
        waveScr.StartCoroutine(waveScr.StartWave());
    }
}
