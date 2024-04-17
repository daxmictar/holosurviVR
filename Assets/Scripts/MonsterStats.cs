using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public RectTransform healthBar;
    public float damageTakenModifier = 1.0f;
    private float originalWidth; // this fixes the width of the health bar to whatever is set in the editor.

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null) 
            originalWidth = healthBar.sizeDelta.x; 
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= (int)(damage * damageTakenModifier);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.sizeDelta = new Vector2(originalWidth * (currentHealth / (float)maxHealth), healthBar.sizeDelta.y);
    }

}
