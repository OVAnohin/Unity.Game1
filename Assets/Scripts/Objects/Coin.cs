using UnityEngine;

public class Coin : SomeObject
{
  protected override void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.GetComponent<Sphere>() != null)
    {
      collision.GetComponent<Sphere>().TakeBonus();
      Die();
    }

    if (IsCollisionDestroyer(collision))
      Die();
  }
}
