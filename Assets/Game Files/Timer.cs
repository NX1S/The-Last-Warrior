using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeLimit = 300f; 
    [SerializeField] float currentTime;
    [SerializeField] TextMeshProUGUI timeUI;
    [SerializeField] Animator animator;
    [SerializeField] GameObject Menu;

    void Start()
    {
        currentTime = timeLimit; 
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay();
        }
        if(currentTime == 0)
        {
            currentTime = 0;
            animator.SetTrigger("Fade");
        }
    }

    void UpdateTimeDisplay()
    {

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);


        timeUI.text = $"{minutes}:{seconds:00}";
    }
}