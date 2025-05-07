using Unity.VisualScripting;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    GameObject player;
    [SerializeField] int Points;
    [SerializeField] AudioSource SFX;
    private bool canScore;
    void Start()
    {
        canScore = true;
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("No GameObject with tag 'Player' found.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Box") && canScore == true)
        {
            canScore = false;
            player.GetComponent<VRPlayer>().AddPoints(Points);
            Destroy(gameObject, 2f);
            SFX.Play();
        }
    }
}
