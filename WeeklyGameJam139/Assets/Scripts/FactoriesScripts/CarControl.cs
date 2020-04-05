using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    [SerializeField] private Sprite Car;
    [SerializeField] private Sprite CarTop;
    [SerializeField] private Car CarObj;

    private int dir;

    private PlayerHealth Player;

    // Start is called before the first frame update
    void Start()
    {
        int col = Random.Range(0, 2);

        if (col == 0)
            GetComponent<SpriteRenderer>().color = Color.red;
        else if (col == 1)
            GetComponent<SpriteRenderer>().color = Color.blue;
        else if (col == 2)
            GetComponent<SpriteRenderer>().color = Color.green;

        if (Definitions.Instance.Directions[dir].Key != 0)
        {
            GetComponent<SpriteRenderer>().sprite = Car;
            if (Definitions.Instance.Directions[dir].Key < 0)
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            else if (Definitions.Instance.Directions[dir].Key > 0)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = CarTop;
            if (Definitions.Instance.Directions[dir].Value < 0)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), -Mathf.Abs(transform.localScale.y));
            else if (Definitions.Instance.Directions[dir].Value > 0)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + CarObj.speed * Definitions.Instance.speedScale * Definitions.Instance.Directions[dir].Key * Time.deltaTime, 
            transform.position.y + CarObj.speed * Definitions.Instance.speedScale *  Definitions.Instance.Directions[dir].Value * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject.GetComponent<PlayerHealth>();
            StartCoroutine(HitPlayer());
        }

        if (collision.gameObject.CompareTag("DeletePoint"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Hit the player when crossing the street and push the player away
    /// </summary>
    /// <returns></returns>
    IEnumerator HitPlayer()
    {
        Debug.Log("Hit player!");

        if (Player != null)
            Player.Health = 0;

        yield return new WaitForSeconds(0.2f);
        Debug.Log("Pass through player");
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void SetDir(int dir1)
    {
        dir = CarObj.dir;
        dir = dir1;
    }
}
