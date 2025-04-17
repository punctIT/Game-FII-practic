using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    public bool haveFlashLight=false;
    public bool elevatorKey = false;
    // aici mai adaugi ce vrei: carduri, iteme, etc.
}
