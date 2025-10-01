using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Action HandleAttack;

    [SerializeField] private BuildingData buildingData;

    public float fireRateTimer;
    public float fireRateTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HandleAttack = FireNormalBullet;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttack();
    }

    private void FireNormalBullet()
    {

    }
}
