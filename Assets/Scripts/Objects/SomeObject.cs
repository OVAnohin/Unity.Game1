using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SomeObject : MonoBehaviour
{
  protected abstract void OnTriggerEnter2D(Collider2D collision);

  protected bool IsCollisionDestroyer(Collider2D collision)
  {
    return collision.GetComponent<Destroyer>() != null;
  }
  
  protected void Die()
  {
    gameObject.SetActive(false);
  }
}
