using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    class Interactable : MonoBehaviour
    {
        public event EventHandler eventa;
        public void Interact()
        {
            eventa(this, new EventArgs());
        }
    }
}
