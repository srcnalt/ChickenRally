using UnityEngine;

public class Collectable : MonoBehaviour, ICollidable
{
    private GameObject touchedObject;

    public void OnTriggerEnter(Collider col)
    {
        touchedObject = col.gameObject;

        React();
    }

    public void React()
    {
        touchedObject.GetComponent<Score>().TotalScore += 10;
        UIManager.instance.score.GetComponent<Animator>().Play("Score");

        gameObject.SetActive(false);
    }

	void OnBecameInvisible() {
		gameObject.SetActive(false);
	}
}
