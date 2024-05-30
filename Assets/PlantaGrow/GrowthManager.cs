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

    void Start()
    {
        // Inicializa con la semilla
        currentPlantStage = Instantiate(seedPrefab, transform.position, transform.rotation);
        StartCoroutine(GrowthCycle());
    }

    IEnumerator GrowthCycle()
    {
        // Espera y cambia las etapas
        yield return new WaitForSeconds(5); 
        ChangePlantStage(babyPlantPrefab);

        yield return new WaitForSeconds(8); 
        ChangePlantStage(youngPlantPrefab);

        yield return new WaitForSeconds(10); 
        ChangePlantStage(maturePlantPrefab);

        yield return new WaitForSeconds(10); 
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
