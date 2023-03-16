using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae.UI
{
    public class FacingCamera : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
