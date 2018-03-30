using UnityEngine;

public class Target : MonoBehaviour 
{
	public float health = 50.0f;

	public void TakeDamege (float amount)
	{
		health -= amount;
		if (health <= 0f) 
		{
			Die ();
		}
	}

	public void Die()
	{
		Destroy (gameObject);
	}
}
