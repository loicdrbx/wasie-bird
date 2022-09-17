using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject preFab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;

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
        // Spawn pipe at spawner GameObject's default position, no rotation
        GameObject pipes = Instantiate(preFab, transform.position, Quaternion.identity);

        //  Randomly shift pipes's vertical position
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
