using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText; 
    [SerializeField] private Button closeButton;

    void Awake()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Hide);
        }
        else
        {
            Debug.LogError("CloseButton is not assigned in Popup!");
        }

       
        Hide();
    }

    public void Show(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
        else
        {
            Debug.LogError("MessageText is not assigned in Popup!");
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}