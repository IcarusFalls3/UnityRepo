using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class AssualtRifle : Firearms
    {
        protected override void Reload()
        {
            Debug.Log("Shooting");
        }

        protected override void Shootting()
        {
            throw new NotImplementedException();
        }
    }
}
