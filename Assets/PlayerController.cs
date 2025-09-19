using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameManager {

    public float moveSpeed = 8f;
    public float regularJumpForce = 10f;
    public float jumpForce = 15f;
    public float jumpCooldown = 1f;
    public GameObject gameHUD;
    public GameObject deathScreen;
    public GameObject victoryScreen;
    public Rigidbody2D rb;

    private float moveX;
    private bool isGrounded = false;
    private bool isInvulnerable;
    private bool isDead = false;
    [SerializeField] private float invincibilityDurationSeconds;
    [SerializeField] private float invincibilityDeltaTime;

    public GameManager fm;
    SoundEffectManager sfxManager;

    Animator animator;

    private void Awake() {
        sfxManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<SoundEffectManager>();
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.SetFloat("yVelocity", rb.velocity.y);
        if (!isDead) {
            moveX = Input.GetAxis("Horizontal") * moveSpeed;
            if (Input.GetButton("Jump") && isGrounded  && fm.fuelPercent >= 3) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (isGrounded) {
                rb.velocity = new Vector2(rb.velocity.x, regularJumpForce);
            }

            if (fm.fuelPercent == 0) KillState();
        }
        

        if (Input.GetKeyDown("escape")) Application.Quit();
    }

    private void FixedUpdate() {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isDead) {
            if (collision.gameObject.CompareTag("Platform")) { 
                foreach (ContactPoint2D contact in collision.contacts) {
                    if (contact.normal.y > 0.5f) { 
                        isGrounded = true;
                        animator.SetBool("isGrounded", isGrounded);
                        break;
                    }
                }
        }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (!isDead) {
            if (collision.gameObject.CompareTag("Platform")) {
                if (Input.GetButton("Jump") && isGrounded && fm.fuelPercent >= 6) { 
                    sfxManager.jump2SFX();
                    fm.fuelPercent -= 4f;
                }
                else if (isGrounded) {
                    sfxManager.jump1SFX();
                }
                isGrounded = false;
                animator.SetBool("isGrounded", isGrounded);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isDead) { 
            if(collision.gameObject.CompareTag("SmallFuel") || collision.gameObject.CompareTag("BigFuel")) {
                Destroy(collision.gameObject);
                if (collision.gameObject.CompareTag("BigFuel")) {
                    fm.fuelPercent += 15f;
                    sfxManager.collectFuel2SFX();
                }
                else { 
                    fm.fuelPercent += 7.5f;
                    sfxManager.collectFuel1SFX();
                }
            }
            else if(collision.gameObject.CompareTag("Gemstone")) {
                Destroy(collision.gameObject);
                fm.gemstoneCount += 1;
                sfxManager.collectGemSFX();
                if (fm.gemstoneCount == 10) VictoryState();
            }
            else if (collision.gameObject.CompareTag("Hurtful")) TakeDamage();

            else if (collision.gameObject.CompareTag("Lethal")) KillState();
        }
    }

    private void TakeDamage() {
        if (isInvulnerable) return;
        fm.fuelPercent -= 5f;
        sfxManager.hitSFX();
        StartCoroutine(BecomeTemporarilyInvincible());
    }

    private IEnumerator BecomeTemporarilyInvincible() {
        isInvulnerable = true;

        StartCoroutine(playerFlash());

        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime) {
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        isInvulnerable = false;
    }

    private IEnumerator playerFlash() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Material defaultMaterial = spriteRenderer.material;
        Material flashMaterial = null;
        Material spritesDefault = Resources.Load<Material>("Sprites-Default");

        bool isInvisible = false;
        
        while(isInvulnerable) {
            if (isInvisible) spriteRenderer.material = defaultMaterial;
            else spriteRenderer.material = flashMaterial;
            
            isInvisible = !isInvisible;
            yield return new WaitForSeconds(0.05f);

            spriteRenderer.material = defaultMaterial;
        }
    }

    private void KillState() {
        isDead = true;
        gameHUD.SetActive(false);
        deathScreen.SetActive(true);
        sfxManager.deathSoundSFX();
    }

    private void VictoryState() {
        gameHUD.SetActive(false);
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
