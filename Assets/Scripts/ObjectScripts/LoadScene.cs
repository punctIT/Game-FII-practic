using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public InventoryData inventory;
    public void LoadScene()
    {
        inventory.playerPosititon=new Vector3(-16,1,50);
        SceneManager.LoadScene("Act2");
    }
}
