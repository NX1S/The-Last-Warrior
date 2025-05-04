using UnityEngine;
using TMPro;

public class SessionManager : MonoBehaviour
{
    [SerializeField] float timeLimit = 300f;
    [SerializeField] float currentTime;
    [SerializeField] TextMeshProUGUI timeUI;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] Animator UIAnimator;
    [SerializeField] GameObject MenuUI;
    [SerializeField] TextMeshPro
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera handsOnlyCamera;

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
        if (currentTime <= 0)
        {
            currentTime = 0;
            Hide();
        }
    }

    void UpdateTimeDisplay()
    {

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);


        timeUI.text = $"{minutes}:{seconds:00}";
    }

    void Hide()
    {
        // Main Camera: set culling mask to everything except hands
        int layerToRemove = LayerMask.NameToLayer("Hands Only");
        mainCamera.cullingMask &= ~(1 << layerToRemove); // remove the layer
        // Hand Camera: enable it
        handsOnlyCamera.enabled = true;
        // Fade black screen
        UIAnimator.SetTrigger("Hide");
        // toggle menu UI
        MenuUI.SetActive(true);
    }

    void Show()
    {
        // Main Camera: set culling mask to everything
        int layerToAdd = LayerMask.NameToLayer("Hands Only");
        mainCamera.cullingMask |= (1 << layerToAdd); // add the layer
        // Hand Camera: disable it
        handsOnlyCamera.enabled = false;
        // toggle menu UI
        MenuUI.SetActive(false);
        // Fade black screen
        UIAnimator.SetTrigger("Show");
    }
}