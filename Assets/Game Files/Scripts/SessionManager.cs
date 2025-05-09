using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SessionManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float timeLimit = 300f;
    [SerializeField] float currentTime;
    int ReasontoStop;
    [SerializeField] TextMeshProUGUI timeUI;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI reasonText;
    [SerializeField] TextMeshPro PointsUI;
    [SerializeField] TextMeshPro HealthUI;
    [SerializeField] Animator UIAnimator;
    [SerializeField] GameObject MenuUI;

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera handsOnlyCamera;

    void Start()
    {
        //int Score = score;
        currentTime = timeLimit;
        // Enable the player from earning or losing any more health
        player.GetComponent<VRPlayer>().canLoseHealth = true;
        player.GetComponent<VRPlayer>().canAddPoints = true;
        // Main Camera: set culling mask to everything
        int layerToAdd = LayerMask.NameToLayer("Hands Only");
        mainCamera.cullingMask |= (1 << layerToAdd); // add the layer
        // Hand Camera: disable it
        handsOnlyCamera.enabled = false;
        // toggle menu UI
        MenuUI.SetActive(false);
        // Enable UI on hand
        HealthUI.enabled = true;
        PointsUI.enabled = true;
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
            Hide(1);
        }

        if(Input.GetKey("R"))
        {
            Restart();
        }
        if(Input.GetKey("S"))
        {
            Hide(3);
        }
    }

    void UpdateTimeDisplay()
    {

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);


        timeUI.text = $"{minutes}:{seconds:00}";
    }

    public void Hide(int ReasontoStop)
    {
        if(ReasontoStop == 1)
        {
            reasonText.text = "Time's up!";
        } else if (ReasontoStop == 2)
        {
            reasonText.text = "You're Dead!";
        }else if (ReasontoStop == 3)
        {
            reasonText.text = "Game Over!";
        } else 
        {
            reasonText.text = "How did we get here?";
        }
        // Disable the player from earning or losing any more health
        player.GetComponent<VRPlayer>().canLoseHealth = false;
        player.GetComponent<VRPlayer>().canAddPoints = false;
        // Main Camera: set culling mask to everything except hands
        int layerToRemove = LayerMask.NameToLayer("Hands Only");
        mainCamera.cullingMask &= ~(1 << layerToRemove); // remove the layer
        // Hand Camera: enable it
        handsOnlyCamera.enabled = true;
        // Disable UI on hand
        HealthUI.enabled = false;
        PointsUI.enabled = false;
        // Fade black screen
        UIAnimator.SetTrigger("Hide");
        // Update score text
        ScoreText.text = "Your Score:\n" + player.GetComponent<VRPlayer>().Points;
        // toggle menu UI
        MenuUI.SetActive(true);

    }

    public void Show()                                        
    {
        // Enable the player from earning or losing any more health
        player.GetComponent<VRPlayer>().canLoseHealth = true;
        player.GetComponent<VRPlayer>().canAddPoints = true;
        // Main Camera: set culling mask to everything
        int layerToAdd = LayerMask.NameToLayer("Hands Only");
        mainCamera.cullingMask |= (1 << layerToAdd); // add the layer
        // Hand Camera: disable it
        handsOnlyCamera.enabled = false;
        // toggle menu UI
        MenuUI.SetActive(false);
        // Enable UI on hand
        HealthUI.enabled = true;
        PointsUI.enabled = true;
        // Fade black screen
        UIAnimator.SetTrigger("Show");
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}