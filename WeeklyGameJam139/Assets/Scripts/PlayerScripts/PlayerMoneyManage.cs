using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class PlayerMoneyManage : MonoBehaviour
{
    [HideInInspector]
    public int money;
    private float TimeCount = 0f;
    [SerializeField] private TextMeshProUGUI CoinText;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        CoinText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount > 60f)
        {
            ChangeDifficulty();
            TimeCount = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            money += 1;
            CoinText.text = money.ToString();
            Debug.Log("Got money");
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (collision.gameObject.CompareTag("Enemy") && !collision.gameObject.GetComponent<EnemyControl>().give_money)
            {
                money += 1;
                collision.gameObject.GetComponent<EnemyControl>().give_money = true;
                CoinText.text = money.ToString();
            }
        }
    }

    private void ChangeDifficulty()
    {
        Definitions.Instance.speedScale += 0.5f;
    }
}
