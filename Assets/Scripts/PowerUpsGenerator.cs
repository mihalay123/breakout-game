using System.Collections.Generic;
using UnityEngine;

public class PowerUpsGenerator : MonoBehaviour
{
    private List<PowerUp> powerUps;// Array of power-up prefabs
    [Range(0, 1)]
    [SerializeField] float spawnRate = 0.0f; // Probability of spawning a power-up (30%)

    void Start()
    {
        LoadPowerUpConfig();
    }

    private void LoadPowerUpConfig()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("powerups");
        if (jsonFile == null)
        {
            Debug.LogError("Power-up config file not found!");
            return;
        }

        PowerUpConfig config = JsonUtility.FromJson<PowerUpConfig>(jsonFile.text);
        powerUps = config.powerUps;
    }

    private void OnEnable()
    {
        BrickEventManager.OnBrickCrashed += HandleBrickCrash;
    }

    private void OnDisable()
    {
        BrickEventManager.OnBrickCrashed -= HandleBrickCrash;
    }

    private void HandleBrickCrash(Vector3 position)
    {
        // Decide whether to spawn a power-up
        if (Random.value <= spawnRate)
        {
            SpawnPowerUp(position);
        }
    }

    private void SpawnPowerUp(Vector3 position)
    {
        if (powerUps == null || powerUps.Count == 0)
            return;

        float totalWeight = 0f;
        foreach (PowerUp powerUp in powerUps)
        {
            totalWeight += powerUp.spawnChance;
        }

        float randomValue = Random.value * totalWeight;
        float cumulativeWeight = 0f;


        foreach (PowerUp powerUp in powerUps)
        {
            cumulativeWeight += powerUp.spawnChance;
            if (randomValue <= cumulativeWeight)
            {
                // Load the prefab dynamically from Resources
                GameObject prefab = Resources.Load<GameObject>($"PowerUps/{powerUp.prefabName}");
                if (prefab != null)
                {
                    Instantiate(prefab, position, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning($"Prefab {powerUp.prefabName} not found in Resources/PowerUps!");
                }
                break;
            }
        }
    }
}
