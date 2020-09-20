using UnityEngine;

public class Boulder : SomeObject
{
  protected override void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.GetComponent<Sphere>() != null)
    {
      collision.GetComponent<Sphere>().TakeDamage();
      Die();
    }

    if (IsCollisionDestroyer(collision))
      Die();
  }
}
