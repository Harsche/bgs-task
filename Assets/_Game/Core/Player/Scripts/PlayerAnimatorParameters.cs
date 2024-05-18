using UnityEngine;

namespace Game.Player
{
    public static class PlayerAnimatorParameters
    {
        public static readonly int DirectionX = Animator.StringToHash("Facing Direction X");
        public static readonly int DirectionY = Animator.StringToHash("Facing Direction Y");
        public static readonly int Speed = Animator.StringToHash("Speed");
    }
}