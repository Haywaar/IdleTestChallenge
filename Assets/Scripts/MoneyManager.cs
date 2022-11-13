using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private NumberData _money;

    public NumberData Money => _money;

    private void OnMoneyAdded(NumberData value)
    {
        _money += value;
        //send signal - money added
    }
    
    private void OnMoneySpent(NumberData value)
    {
        _money -= value;
        //send signal - money spent
    }
}
