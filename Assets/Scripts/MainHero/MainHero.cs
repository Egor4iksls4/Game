using UnityEngine;

public class MainHero : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _horizontalMove = 0f;
    private bool _fasingRight = true;

    [Header("Player Movement Settings")]
    [Range(0, 10f)] public float speed = 1f;
    [Range(0, 15f)] public float jumpForce = 1f;

    [Space]
    [Header("Double Jumps Settings")]
    public int extraJumpsValue;
    private int _extraJumps;

    [Space]
    [Header("Ground Checker Settings")]
    public bool isGrounded = false;
    [Range(-5f, 5f)] public float checkGroundOffsetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;

    [Space]
    [Header("Player Animations")]
    [SerializeField] private Animator _anim;

    private void Start()
    {
        _extraJumps = extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HeroJump();

        _horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
    }

    private void FixedUpdate()
    {
        HeroMove();
        CheckGround();
        CheckFlip();
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);

        if (colliders.Length > 1) isGrounded = true;
        else isGrounded = false;
    }

    private void HeroMove()
    {
        _rb.velocity = new Vector2(_horizontalMove * 10f, _rb.velocity.y);
    }

    private void HeroJump()
    {
        if (isGrounded) { _extraJumps = extraJumpsValue; }

        if (Input.GetKeyDown(KeyCode.Space) && _extraJumps > 0)
        {
            _rb.velocity = Vector2.up * jumpForce;
            _extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _extraJumps == 0 && isGrounded) { _rb.velocity = Vector2.up * jumpForce; }
    }

    private void Flip()
    {
        _fasingRight = !_fasingRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void CheckFlip()
    {
        if (_fasingRight == false && _horizontalMove > 0) Flip();
        else if (_fasingRight == true && _horizontalMove < 0) Flip();
    }
}
