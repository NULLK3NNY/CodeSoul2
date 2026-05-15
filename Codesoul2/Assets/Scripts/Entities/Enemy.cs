using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : Entity
{
    DDA dda;
    GameManager gm;
    public float hp;
    public GameObject attackCollision;
    float timer = 0;
    public float attackRate = 1;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        dda = gm.GetComponent<DDA>();
        hp = 100;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        FlipSelf(GameObject.FindGameObjectWithTag("Player"));
    }

    private void FixedUpdate()
    {
        if (FindDistanceBetweenVectors(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 10)
        {
            FollowTarget(GameObject.FindGameObjectWithTag("Player"));
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        FollowTarget(GameObject.FindGameObjectWithTag("Player"));
        CheckGround(isFacingRight);
    }

    bool CanAttack()
    {
        
        if (timer >= attackRate)
        {
            timer = 0;
            return true;
        }

        return false;
    }

    void FollowTarget(GameObject target)
    {
        animator.SetBool("IsMoving", true);
        if (transform.position.x > target.transform.position.x)
        {
            rb2D.linearVelocity = new Vector2(-GetSpeed() * Time.deltaTime, rb2D.linearVelocity.y);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(GetSpeed() * Time.deltaTime, rb2D.linearVelocity.y);
        }
    }

    void FlipSelf(GameObject target)
    {
        // Check which direction the player is facing
        if (transform.position.x > target.transform.position.x)
        {
            SetFacingRight(false);
        }
        else
        {
            SetFacingRight(true);
        }

        // Then flip the player, arms and weapon
        if (GetFacingRight())
        {
            rig.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rig.transform.localScale = new Vector2(-1, 1);
        }
    }

    float FindDistanceBetweenVectors(Vector2 v1, Vector2 v2)
    {
        float distance = 0;
        float x;
        float y;
        x = v1.x - v2.x;
        y = v1.y - v2.y;
        x *= x;
        y *= y;
        x += y;
        distance = Mathf.Sqrt(x);
        return distance;
    }

    public void Hurt(float damage)
    {
        hp -= damage;
        gm.RewardPointsOnHit();
        Die();
    }

    void Die()
    {
        if(hp <= 0)
        {
            dda.AddKills();
            gm.RewardPointsOnKill();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(CanAttack())
            {
                Debug.Log("Player hit");
            }
        }
    }
}
