using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    private bool spawned = true;
    [SerializeField] private int dir = 0;
    [SerializeField] private GameObject Car;
    [SerializeField] private GameObject TopCar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
            StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        //Debug.Log("Spawn car");
        spawned = false;

        // Spawn car
        GameObject new_car;

        if (Definitions.Instance.Directions[dir].Key != 0)
            new_car = Instantiate(Car, transform);
        else
            new_car = Instantiate(TopCar, transform);

        new_car.transform.position = transform.position;
        new_car.GetComponent<CarControl>().SetDir(dir);

        yield return new WaitForSeconds(2);
        spawned = true;
        //Debug.Log("Can spawn cars again!");
    }
}
