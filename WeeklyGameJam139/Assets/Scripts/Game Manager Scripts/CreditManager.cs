using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditManager : MonoBehaviour
{
    [SerializeField] private List<string> Roles;
    [SerializeField] private List<string> Credits;
    [SerializeField] private TextMeshProUGUI RoleText;
    [SerializeField] private TextMeshProUGUI CreditText;

    int ind = 0;
    float alpha = 0;
    int phase = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ind < Roles.Count)
        {
            RoleText.text = Roles[ind];
            CreditText.text = Credits[ind];
            RoleCredit();
        }
        else
        {
            // Role thank for playing and set the ind back to 0
        }
    }

    void RoleCredit()
    {
        if (phase == 0)
        {
            if (alpha <= 255)
            {
                RoleText.color = new Color32(255, 255, 255, (byte)alpha);
                CreditText.color = new Color32(255, 255, 255, (byte)alpha);
                alpha += 50 * Time.deltaTime;
            }
            else
            {
                alpha = 255;
                phase = 1;
            }
        }
        else
        {
            if (alpha >= 0)
            {
                RoleText.color = new Color32(255, 255, 255, (byte)alpha);
                CreditText.color = new Color32(255, 255, 255, (byte)alpha);
                alpha -= 50 * Time.deltaTime;
            }
            else
            {
                alpha = 0;
                phase = 0;
                ind++;
            }
        }
    }
}
