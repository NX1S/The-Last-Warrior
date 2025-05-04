using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeLimit = 300f; // 5 minutes in seconds
    [SerializeField] float currentTime;
    [SerializeField] TextMeshProUGUI timeUI;
    [SerializeField] Animator animator;

    void Start()
    {
        currentTime = timeLimit; // Start at 5 minutes
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
            Debug.Log("Game ended");
        }
    }

    void UpdateTimeDisplay()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the text in "0:00" format
        timeUI.text = $"{minutes}:{seconds:00}";
    }
}