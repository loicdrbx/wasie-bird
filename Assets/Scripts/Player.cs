
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;
    public float tilt = 5f;

    public AudioClip wingSound;
    public AudioClip dieSound;
    public AudioClip hitSound;
    public AudioClip pointSound;

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
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Move player up following spacebar press, left mouse click, or up arrow
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.up * strength;
            AudioSource.PlayClipAtPoint(wingSound, Vector3.zero);
        }
        // Move player up following screen touch
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began) 
            {
                direction = Vector3.up * strength;
                AudioSource.PlayClipAtPoint(wingSound, Vector3.zero);
            }
        }

        // Fall toward the ground
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt in direction of vertical motion
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
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
            AudioSource.PlayClipAtPoint(hitSound, Vector3.zero);
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
            AudioSource.PlayClipAtPoint(pointSound, Vector3.zero);
        }

    }
}
