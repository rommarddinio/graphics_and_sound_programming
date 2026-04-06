using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BonusManager : MonoBehaviour
{
    public static BonusManager Instance;

    [Header("Менеджеры")]
    public CoinManager coinManager;

    [Header("Настройки бонуса")]
    public float bonusDuration = 5f;

    [Header("Выбор бонуса")]
    public int selectedBonusIndex = 0;

    [Header("Материалы бонусов")]
    public List<Material> bonusMaterials;

    private bool isBonusActive = false;
    private Coroutine bonusCoroutine;
    private float bonusEndTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateBonusAppearance();
    }

    public void SelectBonus(int index)
    {
        selectedBonusIndex = index;
        UpdateBonusAppearance();
    }

    public void UpdateBonusAppearance()
    {
        if (bonusMaterials == null || bonusMaterials.Count == 0) return;

        if (selectedBonusIndex < 0 || selectedBonusIndex >= bonusMaterials.Count)
        {
            return;
        }

        Material targetMat = bonusMaterials[selectedBonusIndex];

        Bonus[] foundBonuses = FindObjectsOfType<Bonus>(true);
        int updatedCount = 0;

        foreach (var b in foundBonuses)
        {
            if (b == null) continue;

            Renderer[] rends = b.GetComponentsInChildren<Renderer>(true);
            foreach (var r in rends)
            {
                r.material = targetMat;
            }
            updatedCount++;
        }

    }

    public void ActivateBonus()
    {
        bonusEndTime = Time.time + bonusDuration;

        if (bonusCoroutine != null)
            StopCoroutine(bonusCoroutine);

        bonusCoroutine = StartCoroutine(BonusRoutine());
    }

    private IEnumerator BonusRoutine()
    {
        isBonusActive = true;
        if (coinManager != null) coinManager.SetBonus(true);

        while (Time.time < bonusEndTime)
            yield return null;

        isBonusActive = false;
        if (coinManager != null) coinManager.SetBonus(false);
        bonusCoroutine = null;
    }

    public float RemainingBonusTime() => isBonusActive ? Mathf.Max(0f, bonusEndTime - Time.time) : 0f;
    public bool IsBonusActive() => isBonusActive;
    public void DeactivateBonus() => isBonusActive = false;
}
