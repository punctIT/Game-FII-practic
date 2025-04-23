using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    public bool haveFlashLight = false;
    public bool elevatorKey = false;
    public Vector3 playerPosititon;
    public string scene = "Act1";

    [System.Serializable]
    private class InventorySaveData
    {
        public bool haveFlashLight;
        public bool elevatorKey;
        public Vector3 playerPosititon;
        public string scene;
    }

    private string GetSavePath(int slot) =>
        Path.Combine(Application.persistentDataPath, $"inventory_save_{slot}.json");

    public void Save(int slot)
    {
        Debug.Log($"SAVE APELAT pe slotul {slot}");

        try
        {
            InventorySaveData data = new InventorySaveData
            {
                haveFlashLight = this.haveFlashLight,
                elevatorKey = this.elevatorKey,
                playerPosititon = this.playerPosititon,
                scene = this.scene
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSavePath(slot), json);
            Debug.Log($"Inventory salvat în slotul {slot} la: {GetSavePath(slot)}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Eroare la salvare: " + ex.Message);
        }
    }

    public void Load(int slot)
    {
        string path = GetSavePath(slot);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

            this.haveFlashLight = data.haveFlashLight;
            this.elevatorKey = data.elevatorKey;
            this.playerPosititon = data.playerPosititon;
            this.scene = data.scene;

            Debug.Log($"Inventory încărcat din slotul {slot} de la: {path}");
        }
        else
        {
            Debug.LogWarning($"Nu există fișier de save pentru slotul {slot} la: {path}");
        }
    }
    public bool SaveSlotExists(int slot)
    {
        return File.Exists(Path.Combine(Application.persistentDataPath, $"inventory_save_{slot}.json"));
    }

}
