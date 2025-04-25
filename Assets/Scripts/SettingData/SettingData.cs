using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "NewSettingsData", menuName = "Settings/SettingsData")]
public class SettingsData : ScriptableObject
{
    [Range(0f, 1f)] public float volume = 0.5f;
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;

    [System.Serializable]
    private class SettingsSaveData
    {
        public float volume;
        public float sensitivityX;
        public float sensitivityY;
    }

    private string GetSavePath(int slot) =>
        Path.Combine(Application.persistentDataPath, $"settings_save_{slot}.json");

    public void Save(int slot)
    {
        Debug.Log($"SALVARE SETĂRI - Slot {slot}");

        try
        {
            SettingsSaveData data = new SettingsSaveData
            {
                volume = this.volume,
                sensitivityX = this.sensitivityX,
                sensitivityY = this.sensitivityY
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSavePath(slot), json);
            Debug.Log($"Setări salvate în slotul {slot} la: {GetSavePath(slot)}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Eroare la salvarea setărilor: " + ex.Message);
        }
    }

    public void Load(int slot)
    {
        string path = GetSavePath(slot);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsSaveData data = JsonUtility.FromJson<SettingsSaveData>(json);

            this.volume = data.volume;
            this.sensitivityX = data.sensitivityX;
            this.sensitivityY = data.sensitivityY;

            Debug.Log($"Setări încărcate din slotul {slot} de la: {path}");
        }
        else
        {
            Debug.LogWarning($"Nu există fișier de setări pentru slotul {slot} la: {path}");
        }
    }

    public bool SaveSlotExists(int slot)
    {
        return File.Exists(GetSavePath(slot));
    }
}
