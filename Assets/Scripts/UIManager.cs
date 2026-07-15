using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;

    private GameObject FloorEndCanvas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        FloorEndCanvas = transform.Find("EndFloorCanvas").gameObject;
    }

    public void UpdateScore(float score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void ShowFloorEndMessage()
    {
        FloorEndCanvas.SetActive(true);
    }

    public void HideFloorEndMessage()
    {
        FloorEndCanvas.SetActive(false);
    }
}
