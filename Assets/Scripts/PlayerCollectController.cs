using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerCollectController : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI tm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tm.SetText("Score: " + score.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fruits") 
        {
            score++;
            StartCoroutine(PlayUntilDestroy(collision.gameObject));
        }
    }

    IEnumerator PlayUntilDestroy(GameObject fruit)
    {
        AudioSource sound = fruit.GetComponent<AudioSource>();
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(fruit);
    }
}
