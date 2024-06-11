using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject babyPlantPrefab;
    public GameObject youngPlantPrefab;
    public GameObject maturePlantPrefab;
    public GameObject fruitPlantPrefab;

    private GameObject currentPlantStage;
    private bool hasStartedGrowth = false;

    void Start()
    {
        // Inicializa con la semilla
        currentPlantStage = Instantiate(seedPrefab, transform.position, transform.rotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona tiene el tag "Seed"
        if (collision.gameObject.tag == "Seed" && !hasStartedGrowth)
        {
            hasStartedGrowth = true;
            StartCoroutine(GrowthCycle());
            Destroy(collision.gameObject); // Destruye la esfera
        }
    }

    IEnumerator GrowthCycle()
    {
        // Espera y cambia las etapas
        yield return new WaitForSeconds(2);
        ChangePlantStage(babyPlantPrefab);

        yield return new WaitForSeconds(2);
        ChangePlantStage(youngPlantPrefab);

        yield return new WaitForSeconds(2);
        ChangePlantStage(maturePlantPrefab);

        yield return new WaitForSeconds(2);
        ChangePlantStage(fruitPlantPrefab);
    }

    void ChangePlantStage(GameObject newStagePrefab)
    {
        if (currentPlantStage != null)
        {
            Destroy(currentPlantStage);
        }
        currentPlantStage = Instantiate(newStagePrefab, transform.position, transform.rotation);
    }
}