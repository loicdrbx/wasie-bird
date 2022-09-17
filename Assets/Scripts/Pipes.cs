using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
 
        // Destroy pipe gameObject after it passes the left edge of the screen
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
