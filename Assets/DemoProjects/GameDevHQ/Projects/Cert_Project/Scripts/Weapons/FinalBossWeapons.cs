using System.Collections;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Cert_Project.Scripts.Weapons
{
    public class FinalBossWeapons : Weapons
    {
        [SerializeField] private float slowCooldown;
        [SerializeField] private float midCooldown;
        [SerializeField] private float fastCooldown;

        protected override void SetInitialValues()
        {
            ResetWeaponPositions();
            weaponsIndex = maxWeaponsIndex;
            UpdateWeaponPositions(weaponsIndex);
            ShootWeapons();
        }

        private void OnEnable()
        {
            Enemy.Enemy.OnBossStateChanged += UpdateShootCooldown; // TODO modify cooldown
            Enemy.Enemy.OnShootingStateChanged += ChangeShootingState;
        }

        private void OnDisable()
        {
            Enemy.Enemy.OnBossStateChanged -= UpdateShootCooldown;
            StopCoroutine(nameof(ShootingRoutine));
        }

        private void UpdateShootCooldown(int enemyStateArg)
        {
            switch (enemyStateArg)
            {
                case 3:
                    shootCooldown = slowCooldown;
                    weaponsIndex = 0;
                    break;
                case 2:
                    shootCooldown = midCooldown;
                    weaponsIndex = 1;
                    break;
                case 1:
                    shootCooldown = fastCooldown;
                    weaponsIndex = 2;
                    break;
                default:
                    Debug.LogWarning("Set a valid cooldown value");
                    break;
            }

            UpdateWeaponPositions(weaponsIndex);
        }

        private void ShootWeapons() => StartCoroutine(nameof(ShootingRoutine));

        // Set cool down
        // FireWeapons()
        private IEnumerator ShootingRoutine()
        {
            while (gameObject.activeInHierarchy && shootEnabled)
            {
                yield return new WaitForSeconds(shootCooldown);
                // TODO shoot mechanic: instantiate lasers in firepoint 0
            }
        }
    }
}
