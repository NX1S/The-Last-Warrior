
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float Sensitivity;
    float XRotation = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        Mouse();
    }

    void Mouse()
    {
        float rotX = Input.GetAxisRaw("Mouse X") * Sensitivity * Time.deltaTime;
        float rotY = Input.GetAxisRaw("Mouse Y") * Sensitivity * Time.deltaTime;

        XRotation = XRotation - rotY;
        XRotation = Mathf.Clamp(XRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);

        Player.transform.Rotate(Vector3.up * rotX);



    }
}
