using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    
    public void SetVal(float value)
    {
        var max = healthBar.anchorMax;
        max.x = value;
        healthBar.anchorMax = max;
    }
}
