using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string GetPath(int slot) =>
        Path.Combine(Application.persistentDataPath, $"slot_{slot}.json");

    public static void Save(GameData data, int slot)
    {
        data.lastSaveDate = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(GetPath(slot), json);
        Debug.Log($"[SaveSystem] Slot {slot} guardado → {GetPath(slot)}");
    }

    public static GameData Load(int slot)
    {
        string path = GetPath(slot);
        if (!File.Exists(path))
        {
            Debug.Log($"[SaveSystem] Slot {slot} vacío.");
            return null;
        }
        string json = File.ReadAllText(path);
        Debug.Log($"[SaveSystem] Slot {slot} cargado.");
        return JsonUtility.FromJson<GameData>(json);
    }

    public static void Delete(int slot)
    {
        string path = GetPath(slot);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"[SaveSystem] Slot {slot} eliminado.");
        }
    }

    public static bool SlotExists(int slot) =>
        File.Exists(GetPath(slot));
}
