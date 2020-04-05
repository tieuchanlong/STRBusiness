using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dash_speed = 10f;
    [SerializeField] private Animator anim;
    private float standardSpeed = 5f;
    private float stamina = 100f;
    private List<Item> effects;
    private List<bool> addedEffects; // Check if add effect already

    // Start is called before the first frame update
    void Start()
    {
        standardSpeed = speed;
        effects = new List<Item>();
        addedEffects = new List<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<PlayerHealth>().Dead)
        {
            float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * Time.deltaTime;

            SwapAnimDirection(horizontal);
            StaminaManage();
            CheckEffects();

            if (Input.GetButtonDown("Dash") && stamina > 0f)
                StartCoroutine(Dash());

            if (standardSpeed >= dash_speed)
                transform.position = new Vector2(transform.position.x + horizontal * standardSpeed, transform.position.y + vertical * standardSpeed);
            else
                transform.position = new Vector2(transform.position.x + horizontal * standardSpeed / StaminaScale(), transform.position.y + vertical * standardSpeed / StaminaScale());
        }
    }

    private void SwapAnimDirection(float horizontal)
    {
        if (horizontal != 0)
        {
            anim.SetBool("walk", true);
            transform.localScale = new Vector2(((horizontal < 0) ? -1 : 1) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
            anim.SetBool("walk", false);

        if (horizontal == 0) return;
    }

    private float StaminaScale()
    {
        return (Mathf.Max((100 - stamina) / 50, 1f));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!collision.gameObject.GetComponent<EnemyControl>().Chase)
                collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (collision.gameObject.CompareTag("Enemy") && !collision.gameObject.GetComponent<EnemyControl>().Chase)
            {
                collision.gameObject.GetComponent<EnemyControl>().triggered = true;
                //collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void StaminaManage()
    {
        // Increase the stamina based on the amount of stamina you have
        stamina = stamina + Mathf.Max(stamina, 10) / 50 * Time.deltaTime;
        stamina = Mathf.Min(stamina, 100);
        anim.SetFloat("stamina", stamina);
        // Play different animations for different staminas
    }

    IEnumerator Dash()
    {
        Debug.Log("Dash!");
        standardSpeed = dash_speed;
        stamina -= 20f;
        stamina = Mathf.Max(stamina, 0);
        anim.SetBool("dash", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("dash", false);
        standardSpeed = speed;
    }

    public void AddEffect(Item effect)
    {
        effects.Add(effect);
        addedEffects.Add(false);
    }

    private void CheckEffects()
    {
        for (int i = 0;i < effects.Count; i++)
        {
            standardSpeed += ((addedEffects[i]) ? 0 : 1) * effects[i].speed_up;
            effects[i].effect_time -= Time.deltaTime;
            effects[i].effect_time = Mathf.Max(effects[i].effect_time, 0);

            if (effects[i].effect_time <= 0f)
            {
                standardSpeed -= effects[i].speed_up;
                effects.RemoveAt(i);
                addedEffects.RemoveAt(i);
                i--;
            }
        }
    }
}
