
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;
    private Quaternion rotation;
    public float gravity = -9.8f;
    public float strength = 5f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        rotation = transform.rotation;
        position.y = 0f;
        rotation.z = 0f;
        transform.position = position;
        transform.rotation = rotation;
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Move player up following spacebar press or left mouse click
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        // Move plater up following screen touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began) 
            {
                direction = Vector3.up * strength;
            }
        }

        // Fall toward the ground
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt nose in dirction of vertical movement
        if (direction.y > 0)
        {
            rotation.z = direction.y * 0.02f;
            transform.rotation = rotation;
        }
        else
        {
            rotation.z = direction.y * 0.02f;
            transform.rotation = rotation;
        }
    }

    private void AnimateSprite()
    {
        if (++spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }

    }
}
