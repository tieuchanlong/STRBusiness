using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TrashControl : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    private bool pickedUp = false;
    private PlayerControl _playerControl;

    [Inject]
    public void Construct(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !pickedUp)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (Input.GetButtonDown("Interact"))
                StartCoroutine(PickUpTrash());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator PickUpTrash()
    {
        Debug.Log("Picked up Trash!");
        int empty = Random.Range(0, 100);

        if (empty <= 40)
        {
            int trash = Random.Range(0, items.Count);
            _playerControl.AddEffect(items[trash]);
            Debug.Log("Picked up trash " + trash);
        }
        else
            Debug.Log("Emoty trash can!");
        pickedUp = true;

        yield return new WaitForSeconds(15);
        pickedUp = false;

    }
}
