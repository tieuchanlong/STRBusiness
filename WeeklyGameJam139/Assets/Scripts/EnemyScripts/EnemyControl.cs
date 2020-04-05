using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyControl : MonoBehaviour
{
    public bool triggered = false;
    private bool chase = false;
    public bool give_money = false;
    private CoinFactory _coinFactory;

    public bool Chase
    {
        get
        {
            return chase;
        }
    }

    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform Player;
    [SerializeField] private Animator anim;

    [Inject]
    public void Construct(CoinFactory coinFactory)
    {
        _coinFactory = coinFactory;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > Player.transform.position.x)
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        else if (transform.position.x < Player.transform.position.x)
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

        if (triggered)
        {
            triggered = false;
            StartCoroutine(WaitMove());
        }

        if (chase)
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Definitions.Instance.speedScale * Time.deltaTime);
    }

    IEnumerator WaitMove()
    {
        Debug.Log("Check Money");
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        anim.SetBool("happy", true);
        yield return new WaitForSeconds(2);
        anim.SetBool("happy", false);
        anim.SetBool("angry", true);
        yield return new WaitForSeconds(3);
        Debug.Log("Chase Player!");
        chase = true;
        anim.SetBool("angry", false);
        anim.SetBool("chase", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !collision.gameObject.GetComponent<EnemyControl>().chase && !chase)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limit") && !chase)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Die()
    {
        chase = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log("Enemy dying and drop money!");
        anim.SetBool("dead", true);

        // Play dead animations 
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Enemy dead and disappear");

        // Drop money and Destroy yourself
        _coinFactory.ProduceCoin(transform.position);
        Destroy(gameObject);
    }
}
