using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthModified : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject babyPlantPrefab;
    public GameObject youngPlantPrefab;
    public GameObject fruitPlantPrefab;
    public GameObject rottenPlantPrefab;

    public int rows = 5; // Número de filas
    public int columns = 5; // Número de columnas
    public float spacing = 2.0f; // Espaciado entre plantas

    private List<GameObject> currentPlantStages = new List<GameObject>();
    private bool hasStartedGrowth = false;
    private bool isFruitStage = false; // Variable para verificar si estamos en la etapa de fruitPlant

    void Start()
    {
        // Calcular el inicio de la posición para centrar la cuadrícula
        Vector3 startPosition = new Vector3(
            transform.position.x - (columns - 1) * spacing / 2,
            transform.position.y,
            transform.position.z - (rows - 1) * spacing / 2
        );

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(
                    startPosition.x + j * spacing,
                    transform.position.y,
                    startPosition.z + i * spacing
                );
                GameObject seed = Instantiate(seedPrefab, position, transform.rotation);
                currentPlantStages.Add(seed);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Seed" && !hasStartedGrowth)
        {
            hasStartedGrowth = true;
            StartCoroutine(GrowthCycle());
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "harvest" && isFruitStage)
        {
            HarvestAllPlants();
        }
    }

    IEnumerator GrowthCycle()
    {
        yield return new WaitForSeconds(10);
        ChangeAllPlantStages(babyPlantPrefab);

        yield return new WaitForSeconds(10);
        ChangeAllPlantStages(youngPlantPrefab);

        yield return new WaitForSeconds(10);
        ChangeAllPlantStages(fruitPlantPrefab);
        isFruitStage = true; // Cambiar el estado a fruitPlant

        yield return new WaitForSeconds(60);
        ChangeAllPlantStages(rottenPlantPrefab);
        isFruitStage = false; // Resetear el estado después de fruitPlant
    }

    void ChangeAllPlantStages(GameObject newStagePrefab)
    {
        for (int i = 0; i < currentPlantStages.Count; i++)
        {
            if (currentPlantStages[i] != null)
            {
                Vector3 position = currentPlantStages[i].transform.position;
                Quaternion rotation = currentPlantStages[i].transform.rotation;

                // Rotar 180 grados en el eje Z para babyPlantPrefab y youngPlantPrefab
                if (newStagePrefab == babyPlantPrefab || newStagePrefab == youngPlantPrefab)
                {
                    rotation *= Quaternion.Euler(0, 0, 180);
                }

                Destroy(currentPlantStages[i]);
                currentPlantStages[i] = Instantiate(newStagePrefab, position, rotation);
            }
        }
    }

    void HarvestAllPlants()
    {
        for (int i = 0; i < currentPlantStages.Count; i++)
        {
            if (currentPlantStages[i] != null)
            {
                Destroy(currentPlantStages[i]);
            }
        }
        currentPlantStages.Clear();
    }
}
