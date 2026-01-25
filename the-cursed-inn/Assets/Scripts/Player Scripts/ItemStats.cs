using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public Sprite image;
    public string itemName;
    public string description;
    public int cost;
    public int quantity;
    public Category itemCat;
    public int weaponActiveTime;

    [System.Serializable]
    public class Melee
    {
        public int damage;
        public float activetimer;
        public float knockbackSpeed;
    }

    public class Projectile
    {
        public int damage;
        public float activetimer;
        public float knockbackSpeed;
        public float speed;
        public GameObject Ammo;
        public bool hasAmmo;
    }
}

