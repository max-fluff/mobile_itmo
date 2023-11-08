using System.Collections.Generic;
using UnityEngine;

namespace Source
{
    [CreateAssetMenu(menuName = "PlayerSkins")]
    public class PlayerSkins : ScriptableObject
    {
        public List<Material> Skins;
    }
}