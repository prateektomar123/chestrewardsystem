using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ChestController chestController;
    [SerializeField] private Button generateChestButton;

    void Awake()
    {
        generateChestButton.onClick.AddListener(OnGenerateChest);
    }

    private void OnGenerateChest()
    {
        chestController.GenerateChest();
    }
}