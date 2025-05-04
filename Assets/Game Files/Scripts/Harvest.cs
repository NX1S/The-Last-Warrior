using UnityEngine;

public class Harvest : MonoBehaviour
{
    GameObject player;
    [SerializeField] int Points;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("No GameObject with tag 'Player' found.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Box"))
        {
            player.GetComponent<VRPlayer>().AddPoints(Points);
        }
    }
}
