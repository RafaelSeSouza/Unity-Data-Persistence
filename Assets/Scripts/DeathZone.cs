using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
