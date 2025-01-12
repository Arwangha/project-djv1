using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;

    
    public void SetVal(float value)
    {
        var max = healthBar.anchorMax;
        max.x = value;
        healthBar.anchorMax = max;
    }
}
