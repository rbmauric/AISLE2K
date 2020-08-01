using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerPowerups : MonoBehaviour
{
    //Attack Powerups
    enum Attack { ChargePunch, None }
    
    //Utility Powerups
    enum Utility { Scanner, None }
    
    //Passive Powerups
    enum Passive { Insulator, None }

    //Powerup Selectors
    delegate void AttackPowerup();
    delegate void UtilityPowerup();
    delegate void PassivePowerup();

}
