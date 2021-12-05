using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float TimerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        TimerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerDisplay >= 0)
        {
            TimerDisplay -= Time.deltaTime;
            if (TimerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        TimerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
