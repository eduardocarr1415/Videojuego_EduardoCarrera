using UnityEngine;

namespace Unity.U2D.Welcome
{
    // Venía con la plantilla. No controla la partida; abre información 2D de Unity.
    [CreateAssetMenu(fileName = "Welcome2DScript", menuName = "Welcome/Welcome2DScript")]
    class Welcome2DScript : ScriptableObject
    {
        public static void Open2DLandingPage()
        {
            Application.OpenURL("https://unity.com/features/2d");
        }
    }
}
