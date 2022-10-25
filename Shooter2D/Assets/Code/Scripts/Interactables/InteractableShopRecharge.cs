using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableShopRecharge : Interactable
{

    public override void Interact(Character character)
    {
        uint _priceMod = character.EquippedGun?.BulletCost ?? 0;
        float? percentage = character.EquippedGun?.GetAmmunitionPercentage();

        if (percentage == 1)
            return;

        int price = (int)(100 * (1f - (percentage ?? 1)));


        if (GameManager.Instance.TryBuy(price))
        {
            character.EquippedGun.RechargeAmmunition();
        }

    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

}
