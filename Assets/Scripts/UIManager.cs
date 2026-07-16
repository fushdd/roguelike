using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Transform persistentHUD;
    private TMP_Text scoreText;
    private TMP_Text floorCounterText;

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

        persistentHUD = transform.Find("PersistentHUD");
        scoreText = persistentHUD.Find("Score").GetComponent<TMP_Text>();
        floorCounterText = persistentHUD.Find("FloorCounter").GetComponent<TMP_Text>();

    }

    public void UpdateScore(float score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateFloor(int floor)
    {
        floorCounterText.text = $"Floor: {floor}";
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
