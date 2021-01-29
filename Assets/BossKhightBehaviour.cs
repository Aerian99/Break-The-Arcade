using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BossKhightBehaviour : MonoBehaviour
{
    Vector2 targetPositionDash;
    public GameObject[] limits;
    int movX;
    private float speed, speedDash;
    public GameObject bulletPrefab, light1, light2, light3;
    public GameObject sliderHealth;
    public GameObject imageBoss;
    Animator anim, m_knightAnim;
    private bool notAttacking, animatingIntro;
    public bool isDashing;
    public int health, maxHealth;
    enum Phases { INITPHASE, PHASE2, PHASE3 }
    Phases phase;
    Rigidbody2D EnemyRB;
    private GameObject m_player;
    float cdIntro, maxCdIntro, cdAttack, cdMaxAttack;
    private GameObject colliderEspada;
    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        colliderEspada = transform.GetChild(0).gameObject;
        colliderEspada.SetActive(false);
        maxCdIntro = 3f;
        cdIntro = maxCdIntro;
        cdMaxAttack = 0.5f;
        cdAttack = cdMaxAttack;
        animatingIntro = true;
        notAttacking = true;
        speed = 350f;
        speedDash = 4000;
        m_player = GameObject.FindGameObjectWithTag("Player");
        light1 = GameObject.Find("LightBoss1");
        light2 = GameObject.Find("LightBoss2");
        light3 = GameObject.Find("LightBoss3");


        anim = transform.parent.GetComponent<Animator>();
        m_knightAnim = gameObject.GetComponent<Animator>();
        phase = Phases.INITPHASE;
        health = maxHealth = 1000;
        EnemyRB = GetComponent<Rigidbody2D>();
        anim.SetBool("finishedTalking", true);
        StartCoroutine(Attacks());
    }
    private void FixedUpdate()
    {
        if (notAttacking && !animatingIntro && !isDashing)
            Move();

        if (!notAttacking && !isDashing)
        {
            NormalAttack();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!animatingIntro)
        {
            if (health > 0)
            {
                if (health < maxHealth / 4)
                {
                    light1.GetComponent<Light2D>().color = new Color(1f, 0, 0);
                    light2.GetComponent<Light2D>().color = new Color(1f, 0, 0);
                    light3.GetComponent<Light2D>().color = new Color(1f, 0, 0);
                    phase = Phases.PHASE3;
                }
                else if (health < maxHealth / 2)
                {
                    light1.GetComponent<Light2D>().color = new Color(1f, 0.352f, 0f);
                    light2.GetComponent<Light2D>().color = new Color(1f, 0.352f, 0f);
                    light3.GetComponent<Light2D>().color = new Color(1f, 0.352f, 0f);
                    phase = Phases.PHASE2;
                }
                else
                {
                    phase = Phases.INITPHASE;
                }
            }
            else
            {
                GameObject.Find("CoinSpawner").GetComponent<CoinWinBoss>().coinSpawner = true;
                GameObject.Find("CoinSpawner (1)").GetComponent<CoinWinBoss>().coinSpawner = true;
                GameObject.Find("CoinSpawner (2)").GetComponent<CoinWinBoss>().coinSpawner = true;
                imageBoss.SetActive(false);
                sliderHealth.SetActive(false);
                EnemyRB.velocity = new Vector2(0, 0);
                anim.SetBool("dead", true);
                GameObject.Find("YouWin").GetComponent<Animator>().SetBool("bossDead", true);
                GameObject.Find("-----SCENEMANAGEMENT").GetComponent<PlaySceneManager>().hasWon = true;
                Destroy(gameObject, 1.8f);
            }

            CheckDirection();
        }

        if (animatingIntro)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            cdIntro -= Time.deltaTime;
            if (cdIntro <= 0)
            {
                sliderHealth = GameObject.Find("HealthbarAlternative");
                imageBoss = GameObject.Find("ImageBoss");
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                animatingIntro = false;
                anim.enabled = false;
                m_knightAnim.SetBool("finishedIntroduction", true);
            }
        }
    }

    void CheckDirection()
    {
        if (m_player.transform.position.x - gameObject.transform.position.x > 0)
            movX = 1;
        else
            movX = -1;



    }
    IEnumerator Attacks()
    {
        while (true)
        {
            if (!animatingIntro)
            {
                switch (phase)
                {
                    case Phases.INITPHASE:
                        int random = Random.Range(0, 2);
                        if (random == 0)
                        {
                            if (!isDashing)
                            {
                                ActivateDash();
                            }
                            
                            if(isDashing)
                            {
                                m_knightAnim.SetBool("isDashing", true);
                                StartCoroutine(Dash());
                                yield return new WaitForSeconds(10);
                            }
                        }
                        else if(random == 1)
                        {
                            StartCoroutine(attackBalls());

                        }
                        //notAttacking = false;
                        yield return new WaitForSeconds(0);
                        break;
                    case Phases.PHASE2:
                        //notAttacking = false;
                        yield return new WaitForSeconds(0);
                        break;
                    case Phases.PHASE3:
                        //notAttacking = false;
                        yield return new WaitForSeconds(0);
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForSeconds(0);
        }



    }

    IEnumerator Dash()
    {

        transform.GetChild(1).gameObject.SetActive(true);
        if (movX == 1)
        {
            targetPositionDash = limits[0].transform.position - gameObject.transform.position;
            targetPositionDash.Normalize();
            EnemyRB.velocity = new Vector2(targetPositionDash.x * Time.deltaTime * speedDash, 0);

            yield return new WaitUntil(() => !isDashing);
            isDashing = true;
            gameObject.transform.localScale = new Vector3(-6.551481f, 6.551481f, 6.551481f);
            EnemyRB.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);
            m_knightAnim.SetBool("isDashing", true);
            targetPositionDash = limits[1].transform.position - gameObject.transform.position;
            targetPositionDash.Normalize();
            EnemyRB.velocity = new Vector2(targetPositionDash.x * Time.deltaTime * speedDash, 0);

            yield return new WaitUntil(() => !isDashing);
            isDashing = true;
            gameObject.transform.localScale = new Vector3(6.551481f, 6.551481f, 6.551481f);
            EnemyRB.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            targetPositionDash = limits[1].transform.position - gameObject.transform.position;
            targetPositionDash.Normalize();
            EnemyRB.velocity = new Vector2(targetPositionDash.x * Time.deltaTime * speedDash, 0);

            yield return new WaitUntil(() => !isDashing);
            isDashing = true;
            gameObject.transform.localScale = new Vector3(6.551481f, 6.551481f, 6.551481f);
            EnemyRB.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);
            m_knightAnim.SetBool("isDashing", true);
            targetPositionDash = limits[0].transform.position - gameObject.transform.position;
            targetPositionDash.Normalize();
            EnemyRB.velocity = new Vector2(targetPositionDash.x * Time.deltaTime * speedDash, 0);

            yield return new WaitUntil(() => !isDashing);
            isDashing = true;
            gameObject.transform.localScale = new Vector3(-6.551481f, 6.551481f, 6.551481f);
            EnemyRB.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);
        }

        gameObject.transform.localScale = new Vector3(6.551481f, 6.551481f, 6.551481f);
        isDashing = false;
        m_knightAnim.SetBool("isDashing", false);
    }

    void ActivateDash()
    {
        isDashing = true;
    }


    IEnumerator attackBalls()
    {
        yield return null;

    }
    void Move()
    {
        Vector2 targetPosition = m_player.transform.position - gameObject.transform.position;
        if (targetPosition.x < 1.5f && targetPosition.x > -1.5f)
            notAttacking = false;
        targetPosition.Normalize();

        EnemyRB.velocity = new Vector2(targetPosition.x * Time.deltaTime * speed, 0);
        if (movX == 1)
            gameObject.transform.localScale = new Vector3(6.551481f, 6.551481f, 6.551481f);
        else
        {
            gameObject.transform.localScale = new Vector3(-6.551481f, 6.551481f, 6.551481f);
        }


    }

    void NormalAttack()
    {
        EnemyRB.velocity = new Vector2(0, 0);
        m_knightAnim.SetBool("isAttacking", true);
        cdAttack -= Time.deltaTime;
        colliderEspada.SetActive(true);
        if (cdAttack <= 0)
        {
            colliderEspada.SetActive(false);
            m_knightAnim.SetBool("isAttacking", false);
            cdAttack = cdMaxAttack;
            notAttacking = true;
        }

    }

    void ActivateSword()
    {
        colliderEspada.SetActive(true);
    }
    void DeActivateSword()
    {
        colliderEspada.SetActive(false);
    }
}
