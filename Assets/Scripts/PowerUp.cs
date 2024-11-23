using System;

[Serializable]
public class PowerUp
{
    public string name; // Name of the power-up
    public string prefabName; // Name of the prefab file in the Resources folder
    public float spawnChance; // Probability of spawning this power-up
}
