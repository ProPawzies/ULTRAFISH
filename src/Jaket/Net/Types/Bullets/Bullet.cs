namespace Jaket.Net.Types;

using UnityEngine;

using Jaket.Content;
using Jaket.IO;

/// <summary> Representation of a rocket or cannonball. </summary>
public class Bullet : OwnableEntity
{
    /// <summary> Bullet position and rotation. </summary>
    private FloatLerp x, y, z, rx, ry, rz;
    /// <summary> Player riding the rocket. </summary>
    private EntityProv<RemotePlayer> player = new();

    /// <summary> Whether the rocket is frozen by the owner. </summary>
    private bool frozen;
    /// <summary> Whether the player is currently riding the rocket. </summary>
    private bool riding;

    private void Awake()
    {
        Init(_ => Bullets.EType(name), true, true);
        InitTransfer(() =>
        {
            if (Rb) Rb.isKinematic = !IsOwner;
            if (Ball) Ball.ghostCollider = !IsOwner;
            if (Grenade) Exploded(!IsOwner);
            player.Id = Owner;
        });

        x = new(); y = new(); z = new();
        rx = new(); ry = new(); rz = new();
    }

    private void Update()
    {
        if (IsOwner || Dead) return;

        if (riding)
        {
            transform.localPosition = Vector3.back;
            transform.localEulerAngles = Vector3.zero;

            if (transform.parent != player.Value.transform) transform.SetParent(player.Value.transform, true);
        }
        else
        {
            transform.position = new(x.Get(LastUpdate), y.Get(LastUpdate), z.Get(LastUpdate));
            transform.eulerAngles = new(rx.GetAngel(LastUpdate), ry.GetAngel(LastUpdate), rz.GetAngel(LastUpdate));

            if (transform.parent != null) transform.SetParent(null, true);
        }
    }

    private void Exploded(bool value) => Tools.Field<Grenade>("exploded").SetValue(Grenade, value);

    #region entity

    public override void Write(Writer w)
    {
        base.Write(w);
        w.Vector(transform.position);
        w.Vector(transform.eulerAngles);

        if (Grenade)
        {
            w.Bool(IsOwner ? Grenade.playerRiding : riding);
            w.Bool(IsOwner ? Grenade.frozen : frozen);
        }
    }

    public override void Read(Reader r)
    {
        base.Read(r);
        x.Read(r); y.Read(r); z.Read(r);
        rx.Read(r); ry.Read(r); rz.Read(r);

        if (Grenade)
        {
            riding = r.Bool();
            Grenade.rocketSpeed = IsOwner ? 100f : (frozen = r.Bool()) ? 98f : 99f;
        }
    }

    public override void Kill()
    {
        if (Grenade) Exploded(false);

        Grenade?.Explode(harmless: true);
        Ball?.Break();
    }

    #endregion
}
