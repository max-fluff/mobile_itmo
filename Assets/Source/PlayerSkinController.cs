using UnityEngine;

namespace Source
{
    public class PlayerSkinController : MonoBehaviour
    {
        [SerializeField] private PlayerSkins skins;
        [SerializeField] private Renderer renderer;

        public void SetSkin(int id)
        {
            renderer.material = skins.Skins[id];
        }
    }
}