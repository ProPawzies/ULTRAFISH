namespace Jaket.Assets;

using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary> Class that works with the assets of the game. </summary>
public class GameAssets
{
    #region content

    /// <summary> List of items that mustn't be synchronized, because they are not items at all. </summary>
    public static readonly string[] ItemExceptions = new[]
    { "Minotaur", "Tram (3)", "BombTrigger", "BombStationTramTeleporterKey", "Checker" };

    /// <summary> List of internal names of all enemies. </summary>
    public static readonly string[] Enemies = new[]
    {
        "Zombie", "Projectile Zombie", "Super Projectile Zombie", "ShotgunHusk", "MinosBoss", "Stalker", "Sisyphus", "Ferryman",
        "SwordsMachineNonboss", "Drone", "Streetcleaner", "Mindflayer", "V2", "V2 Green Arm Variant", "Turret", "Gutterman",
        "Guttertank", "Spider", "StatueEnemy", "Mass", "Idol", "Mannequin", "Minotaur", "Virtue",
        "Gabriel", "Gabriel 2nd Variant", "Wicked", "Flesh Prison", "DroneFlesh", "Flesh Prison 2", "DroneFleshCamera Variant", "DroneSkull Variant",
        "MinosPrime", "SisyphusPrime", "Cancerous Rodent", "Very Cancerous Rodent", "Mandalore", "Big Johninator", "Puppet"
    };

    /// <summary> List of internal names of all items. </summary>
    public static readonly string[] Items = new[]
    { ".Apple Bait", ".Maurice Bait", "SkullBlue", "SkullRed", "Soap", "Torch", "Florp Throwable" };

    /// <summary> List of internal names of all dev plushies. </summary>
    public static readonly string[] Plushies = new[]
    {
        "Jacob", "Mako", "HEALTH - Jake", "Dalia", "Jericho", "Meganeko", "Tucker", "BigRock", "Dawg", "Sam",
        "Cameron", "Gianni", "Salad", "Mandy", "Joy", "Weyte", "Heckteck", "Hakita", "Lenval", ". (CabalCrow) Variant",
        "Quetzal", "HEALTH - John", "PITR", "HEALTH - BJ", "Francis", "Vvizard", "Lucas", "Scott", "KGC", "."
    };

    /// <summary> List of readable names of all dev plushies needed for the /plushy command. </summary>
    public static readonly string[] PlushiesButReadable = new[]
    {
        "Jacob", "Maximilian", "Jake", "Dalia", "Jericho", "Meganeko", "Tucker", "BigRock", "Victoria", "Samuel",
        "Cameron", "Gianni", "Salad", "Mandy", "Joy", "Weyte", "Heckteck", "Hakita", "Lenval", "CabalCrow",
        "Quetzal", "John", "Pitr", "BJ", "Francis", "Vvizard", "Lucas", "Scott", "KGC", "V1"
    };
    
    /// <summary> List of internal names of all fishes. </summary>
    public static readonly string[] Fishes =
    {
        "Funny Fish!!!", "pitr fish", "Trout", "Amid Efil Fish", "Dusk Chomper",
        "Bomb Fish", "Gib Eyeball Fish", "IronLungFish", "Dope Fish", "Fish Stick",
        "Cooked Fish", "Shark Fish", "Burnt Stuff"
    };

    #endregion
    #region tools

    private static GameObject Prefab(string name) => AssetHelper.LoadPrefab($"Assets/Prefabs/{name}.prefab");

    private static void Material(string name, Action<Material> cons) => Addressables.LoadAssetAsync<Material>($"Assets/Models/{name}.mat").Task.ContinueWith(t => cons(t.Result));

    #endregion
    #region loading

    public static GameObject Enemy(string name) => Prefab($"Enemies/{name}");

    public static GameObject Item(string name) => Prefab($"Items/{name}");

    public static GameObject Bait(string name) => Prefab($"Fishing/{name}");

    public static GameObject Fish(string name) => Prefab($"Fishing/Fishes/{name}");

    public static GameObject Plushy(string name) => Prefab($"Items/DevPlushies/DevPlushie{(name.StartsWith(".") ? name.Substring(1) : $" ({name})")}");

    /// <summary> Loads the torch prefab. </summary>
    public static GameObject Torch() => Prefab("Levels/Interactive/Altar (Torch) Variant");

    /// <summary> Loads the blast explosion prefab. </summary>
    public static GameObject Blast() => Prefab("Attacks and Projectiles/Explosions/Explosion Wave");

    /// <summary> Loads the harmless explosion prefab. </summary>
    public static GameObject Harmless() => Prefab("Attacks and Projectiles/Explosions/Explosion Harmless");

    /// <summary> Loads the shotgun pickup prefab. </summary>
    public static GameObject Shotgun() => Prefab("Weapons/Pickups/ShotgunPickUp");

    /// <summary> Loads the fish pickup prefab. </summary>
    public static GameObject FishTemplate() => Prefab("Fishing/Fish Pickup Template");

    /// <summary> Loads a swordsmachine material by name. </summary>
    public static void SwordsMaterial(string name, Renderer output) => Material($"Enemies/SwordsMachine/{name}", mat => output.material = mat);

    /// <summary> Loads an insurrectionist material by name. </summary>
    public static void SisyMaterial(string name, Renderer[] output) => Material($"Enemies/Sisyphus/{name}", mat => output[0].material = output[1].material = mat);

    #endregion
}