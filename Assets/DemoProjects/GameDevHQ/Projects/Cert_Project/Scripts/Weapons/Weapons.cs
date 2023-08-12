using System.Collections.Generic;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Cert_Project.Scripts.Weapons
{
    public class Weapons : MonoBehaviour
    {
        [SerializeField] protected GameObject[] weaponsPositions;
        [SerializeField] protected List<GameObject> weaponsPrefabs;
        [SerializeField] protected int weaponsIndex;
        [SerializeField] protected int minWeaponsIndex;
        [SerializeField] protected int maxWeaponsIndex;
        [SerializeField] protected float shootCooldown;
        [SerializeField] protected float timeForNextShoot;
        [SerializeField] protected bool shootEnabled;

        private void Start()
        {
            SetInitialValues();
        }

        protected virtual void SetInitialValues()
        {
            ResetWeaponPositions();
            minWeaponsIndex = 0;
            maxWeaponsIndex = 2;
            weaponsIndex = minWeaponsIndex;
            UpdateWeaponPositions(weaponsIndex);
        }

        protected void ChangeShootingState(bool shootEnabledArg)
        {
            shootEnabled = shootEnabledArg;
        }

        protected void ResetWeaponPositions()
        {
            foreach (var laserPos in weaponsPositions) laserPos.SetActive(false);
        }

        protected void IncreaseWeapons(int value)
        {
            weaponsIndex += value;

            if (weaponsIndex > maxWeaponsIndex)
            {
                weaponsIndex = maxWeaponsIndex;
                Debug.Log("weapons added but max value exceeded");
                return;
            }

            UpdateWeaponPositions(weaponsIndex);
        }

        protected void DecreaseWeapons(int value)
        {
            weaponsIndex -= value;

            if (weaponsIndex < minWeaponsIndex)
            {
                weaponsIndex = minWeaponsIndex;
                Debug.Log("weapons reduced but wrong min value");
                return;
            }

            UpdateWeaponPositions(weaponsIndex);
        }

        protected void UpdateWeaponPositions(int indexArg)
        {
            if (weaponsPositions.Length == 1) return;

            switch (indexArg)
            {
                case 0:
                    weaponsPositions[0].SetActive(true);
                    weaponsPositions[1].SetActive(false);
                    weaponsPositions[2].SetActive(false);
                    break;
                case 1:
                    weaponsPositions[0].SetActive(false);
                    weaponsPositions[1].SetActive(true);
                    weaponsPositions[2].SetActive(true);
                    break;
                case 2:
                    weaponsPositions[0].SetActive(true);
                    weaponsPositions[1].SetActive(true);
                    weaponsPositions[2].SetActive(true);
                    break;
            }
        }

        protected void CheckForNextShot()
        {
            if (Time.time > timeForNextShoot)
            {
                timeForNextShoot = Time.time + shootCooldown;
            }
        }

        protected virtual void FireWeapons() => CheckForNextShot();
    }
}
