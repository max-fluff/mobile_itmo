using NaughtyAttributes;
using UnityEngine;

namespace BaranovskyStudio
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Arcade Idle Components/Player Animations", 0)]
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] 
        private bool _useAnotherAnimator;
        [BoxGroup("ANIMATOR")] [ShowIf("_useAnotherAnimator")] [SerializeField] [Required("Drag and drop animator here.")]
        private Animator _animator;

        private const string SPEED_MULTIPLIER = "SpeedMultiplier";
        
        private void Start()
        {
            CheckForErrors();
            if (_useAnotherAnimator == false)
            {
                _animator = GetComponent<Animator>();
            }
        }
        
        private void CheckForErrors()
        {
            if (Resources.Load<Settings>(Constants.SETTINGS).ShowWarnings)
            {
                if (_useAnotherAnimator && _animator == null)
                {
                    Debug.LogError("Player Animations: animator is null.");
                }
            }
        }

        /// <summary>
        /// Update values in animator.
        /// </summary>
        /// <param name="speedMultiplier">Speed coefficient of a player from 0 to 1.</param>
        public void UpdateAnimator(float speedMultiplier)
        {
            _animator.SetFloat(SPEED_MULTIPLIER, speedMultiplier);
        }
    }
}