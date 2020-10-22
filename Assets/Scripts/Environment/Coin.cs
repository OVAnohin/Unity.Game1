using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Sphere>(out Sphere sphere))
        {
            sphere.TakeCoin();
            gameObject.SetActive(false);
        }
    }
}
