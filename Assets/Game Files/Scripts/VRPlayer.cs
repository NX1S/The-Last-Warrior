using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VRPlayer : MonoBehaviour
{
    [SerializeField] float MaxHealth = 20f;
    [SerializeField] public float CurrentHealth = 20f;
    [SerializeField] public float Points = 0f;
    [SerializeField] TextMeshPro PointsUI;
    [SerializeField] TextMeshPro HealthUI;
    [SerializeField] GameObject SessionManager;
    public bool canAddPoints;
    public bool canLoseHealth;

    void Start()
    {
        CurrentHealth = MaxHealth;
        Points = 0;
        canAddPoints = true;
        canLoseHealth = true;
    }   

    public void DecreasePlayerHealth(float Damage)
    {
        //Decreasing the player health
        if (canLoseHealth)
        {
            CurrentHealth -= Damage;
            UpdateUI();
            if( CurrentHealth <= 0)
            {
                SessionManager.GetComponent<SessionManager>().Hide(2);
            }
        }
    }

    public void AddPoints(int amount)
    {
        if (canAddPoints)
        {
            Points += amount;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        PointsUI.text = "SCORE:\n" + Points.ToString();
        HealthUI.text = "HEALTH:\n" + CurrentHealth.ToString();
    }
}
