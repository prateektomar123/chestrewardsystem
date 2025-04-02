using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI coinsText; 
    [SerializeField] private TextMeshProUGUI gemsText; 
   

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
        
    }
    void Start()
    {
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnPlayerDataChanged += UpdateUI;
            UpdateUI(); 
        }
        else
        {
            Debug.LogError("PlayerData instance is null in UIManager!");
        }
    }
    void OnDestroy()
    {
        
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.OnPlayerDataChanged -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        if (PlayerData.Instance == null) return;

        int coins = PlayerData.Instance.GetCoins();
        int gems = PlayerData.Instance.GetGems();

        if (coinsText != null)
        {
            coinsText.text = $"Coins: {coins}";
        }
        else
        {
            Debug.LogError("CoinsText is not assigned in UIManager!");
        }

        if (gemsText != null)
        {
            gemsText.text = $"Gems: {gems}";
        }
        else
        {
            Debug.LogError("GemsText is not assigned in UIManager!");
        }
    }
}