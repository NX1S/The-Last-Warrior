using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class VRPlayer : MonoBehaviour
{
    [SerializeField] float MaxHealth = 20f;
    [SerializeField] public float CurrentHealth = 20f;
    [SerializeField] public float Points = 0f;
    [SerializeField] TextMeshPro PointsUI;
    [SerializeField] TextMeshPro HealthUI;
    public bool canAddPoints;
    public bool canLoseHealth;

    void Start()
    {
        CurrentHealth = MaxHealth;
        Points = 0;
        canAddPoints = true;
        canLoseHealth = true;
    }

    public void DecreasePlayerHealth(int Damage)
    {
        //Decreasing the player health
        if (canLoseHealth)
        {
            CurrentHealth -= Damage;
            UpdateUI();
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
