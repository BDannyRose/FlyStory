using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonus
{
    [System.Serializable]
    public class BonusInv
    {
        public Armor armor;
    }
    public class Bonus
    {
        public string bonusName;
        public string description;
        public bool active;
        public float timer;
        virtual public void Activate()
        {
            active = true;
        }
        virtual public void Deactivate()
        {
            active = false;
        }
    }

    [System.Serializable]
    public class Armor : Bonus
    {

    }
}

public class Bonuses : MonoBehaviour
{
    public Bonus.BonusInv bonuses;

    public void Activate(int ID)
    {
        Bonus.Bonus bonus = RetrieveBonus(ID);
        StartCoroutine(BonusTimer(bonus));
    }

    Bonus.Bonus RetrieveBonus(int ID)
    {
        switch (ID)
        {
            case 1:
                return bonuses.armor;
            default:
                return null;
        }
    }

    IEnumerator BonusTimer(Bonus.Bonus bonus)
    {
        bonus.Activate();
        yield return new WaitForSeconds(bonus.timer);
        bonus.Deactivate();
        yield break;
    }
}
