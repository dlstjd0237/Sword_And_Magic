using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float Power;
    public void Initialized(float AttackPower)
    {
        Power = AttackPower;
        Invoke(nameof(SelfActive), 5);
    }

    private void SelfActive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent<Health>(out Health health))
            {
                health.ApplyeDamage(Power);
                gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        transform.Translate(new Vector3(10 * Time.deltaTime, 0, 0));
    }
    private void OnDisable()
    {
        PoolManager.ReturnToPool(gameObject);
    }
}
