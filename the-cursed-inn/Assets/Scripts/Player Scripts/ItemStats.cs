using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public Sprite image;
    public string itemName;
    public string description;
    public int cost;
    public int quantity;
    public Category itemCat;

    [System.Serializable]
    public class Weapon
    {
        public int damage;
        public float activetimer;
        public float knockbackSpeed;

    }

    public class Melee : Weapon
    {

    }

    public class Projectile : Weapon
    {
        public float speed;
        public GameObject Ammo;
        public bool hasAmmo;
    }
}

