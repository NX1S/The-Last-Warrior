using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float Gravity;
    [SerializeField] float GgroundDistance;
    [SerializeField] float CurrentHealth = 20;
    [SerializeField] int Points = 0;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask GroundMask;
    [SerializeField] TextMeshPro PointsUI;
    bool isGrounded;
    Vector3 velocity;




    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // isGrounded = Physics.CheckSphere(GroundCheck.position, GgroundDistance, GroundMask);
        //if(isGrounded && velocity.y < 0)
        //{
            //velocity.y = -2f;
       // }
        PlayerFall();
        UpdateUI();
    }


    void PlayerFall()
    {
        
        velocity.y = Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void DecreasePlayerHealth(float Damage)
    {
        //Decreasing the player health
        CurrentHealth -= Damage;
    }

    public void UpdateUI()
    {
        PointsUI.text = Points.ToString();
    }

    public void AddPoints(int amount)
    {
        Points += amount;
    }

















}
