using UnityEngine;

public class Collectable : MonoBehaviour, ICollidable
{
    private GameObject touchedObject;
    private AudioSource audioSource;
    public AudioClip[] chickenSounds;

    public void OnTriggerEnter(Collider col)
    {
        touchedObject = col.gameObject;

        React();
    }

    public void React()
    {
        touchedObject.GetComponent<Score>().TotalScore += 10;
        UIManager.instance.score.GetComponent<Animator>().Play("Score");

        audioSource = touchedObject.GetComponent<AudioSource>();
        audioSource.clip = chickenSounds[Random.Range(0, 3)];
        audioSource.Play();

        gameObject.SetActive(false);
    }
}
