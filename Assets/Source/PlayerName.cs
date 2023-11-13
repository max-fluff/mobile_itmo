using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class PlayerName : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameLabel;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = FindFirstObjectByType<Camera>();
        }

        private void Update()
        {
            var delta = transform.position - _camera.transform.position;
            var deltaByX = new Vector3(0, delta.y, delta.z);

            transform.rotation = Quaternion.LookRotation(deltaByX);
        }

        public void SetName(string playerName)
        {
            nameLabel.SetText(playerName);
            LayoutRebuilder.MarkLayoutForRebuild((RectTransform)nameLabel.transform.parent);
        }
    }
}