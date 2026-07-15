using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade
{
    public string name;
    public string description;
    public Action effect;

    public Upgrade(string name, string description, Action effect)
    {
        this.name = name;
        this.description = description;
        this.effect = effect;
    }
}

public class UpgradeManager : MonoBehaviour
{
    private List<Upgrade> upgradeList = new();

    private Transform upgradeChoiceCanvas;
    [SerializeField] private Transform upgradeButton;

    public void StartUpgrading()
    {
        float floorPower = GameManager.Instance.floorPower;
        upgradeChoiceCanvas = transform.GetChild(0);

        float speedStat = GenerateRandomStat(floorPower);
        upgradeList.Add(new Upgrade("Speed", $"Increases your speed by {speedStat}%", () =>
        {
            GameManager.Instance.UpdateSpeedMultiplier(speedStat);
            GameManager.Instance.InitiateNewScene();
        }));

        float damageStat = GenerateRandomStat(floorPower);
        upgradeList.Add(new Upgrade("Damage", $"Increases your damage by {damageStat}%", () =>
        {
            GameManager.Instance.UpdateDamageMultiplier(damageStat);
            GameManager.Instance.InitiateNewScene();
        }));

        float bulletLifespanStat = GenerateRandomStat(floorPower);
        upgradeList.Add(new Upgrade("Bullet Lifespan", $"Increases your bullets' lifespan by {bulletLifespanStat}%", () =>
        {
            GameManager.Instance.UpdateBulletLifespanMultiplier(bulletLifespanStat);
            GameManager.Instance.InitiateNewScene();
        }));

        float bulletSpeedStat = GenerateRandomStat(floorPower);
        upgradeList.Add(new Upgrade("Bullet Speed", $"Increases your bullets' speed by {bulletSpeedStat}%", () =>
        {
            GameManager.Instance.UpdateBulletSpeedMultiplier(bulletSpeedStat);
            GameManager.Instance.InitiateNewScene();
        }));

        float attackCooldownStat = GenerateRandomStat(floorPower);
        upgradeList.Add(new Upgrade("Attack Cooldown", $"Decreases your attack cooldown by {attackCooldownStat}%", () =>
        {
            GameManager.Instance.UpdateAttackCooldownMultiplier(-attackCooldownStat);
            GameManager.Instance.InitiateNewScene();
        }));


        for (int i = 0; i < 3; i++)
        {
            Upgrade randUpgrade = upgradeList[UnityEngine.Random.Range(0, upgradeList.Count)];
            Transform btn = Instantiate(upgradeButton, upgradeChoiceCanvas);

            // move buttons
            btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-250 + (250 * i), 6);
            // set name and description
            btn.Find("Name").GetComponent<TMP_Text>().text = randUpgrade.name;
            btn.Find("Description").GetComponent<TMP_Text>().text = randUpgrade.description;
            // set onclick method
            Button btnButtonComponent = btn.GetComponent<Button>();
            btnButtonComponent.onClick.RemoveAllListeners();
            btnButtonComponent.onClick.AddListener(() => randUpgrade.effect());

            upgradeList.Remove(randUpgrade);
        }


    }

    private float GenerateRandomStat(float floorPower)
    {
        float randomStat = (float)Math.Round(UnityEngine.Random.Range(floorPower, floorPower + 20f));
        return randomStat;
    }
}
