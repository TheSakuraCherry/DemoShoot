using UnityEditor;
using UnityEngine;

namespace MainScript.Base
{
    public static class LayerManagerCopy
    {
        public static int Ragdoll;
        public static int UI;
        public static int Terrain;
        public static int Enemy;
        public static int Water;
        public static int AimTarget;
        public static int Player;
        public static int Bullet;
        public static int Player1P;
        public static int Player2P;

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        public static void Init()
        {
            Ragdoll = LayerMask.NameToLayer(nameof(Ragdoll));
            UI = LayerMask.NameToLayer(nameof(UI));
            Terrain = LayerMask.NameToLayer(nameof(Terrain));
            Enemy = LayerMask.NameToLayer(nameof(Enemy));
            Water = LayerMask.NameToLayer(nameof(Water));
            AimTarget = LayerMask.NameToLayer(nameof(AimTarget));
            Player = LayerMask.NameToLayer(nameof(Player));
            Bullet = LayerMask.NameToLayer(nameof(Bullet));
            Player1P = LayerMask.NameToLayer(nameof(Player1P));
            Player2P = LayerMask.NameToLayer(nameof(Player2P));
        }
        public static void SetLayer(GameObject obj, int layer)
        {
            if (obj == null)
            {
                return;
            }
            obj.layer = layer;

            foreach (Transform child in obj.transform)
            {
                SetLayer(child.gameObject, layer);
            }
        }
    }
   
}