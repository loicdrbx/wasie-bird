using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject preFab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;
    public float minVariance = 0.5f;

    private Vector3 previousPosition;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }


    private void onDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(preFab, transform.position, Quaternion.identity);

        // Spawn new pipes in opposite direction of previous ones
        if (previousPosition.y > 0f)
        {
            pipes.transform.position += Vector3.up * Random.Range(minHeight, -minVariance);
        }
        else
        {
            pipes.transform.position += Vector3.up * Random.Range(minVariance, maxHeight);
        }

        previousPosition = pipes.transform.position;
    }
}
