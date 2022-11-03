using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    [SerializeField]
    TMP_Text currentTime;

    [SerializeField]
    TMP_Text pastTime;

    float timer;
    float seconds;
    float minutes;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        currentTime.text = "00:00";
        pastTime.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        RunTime();
    }

    public void RunTime()
    {
        
    }

    public void setPastTime()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            setPastTime();
            timer = 0;
        }
    }
}
