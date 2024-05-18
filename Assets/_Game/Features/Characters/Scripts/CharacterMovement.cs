using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Characters
{
    public class CharacterMovement
    {
        public float WalkSpeed { get; set; } = 4;

        private List<MoveCommand> _moveCommands = new();

        private readonly MoveCommand _right;
        private readonly MoveCommand _left;
        private readonly MoveCommand _up;
        private readonly MoveCommand _down;
        private readonly MoveCommand _stop;

        public CharacterMovement(Rigidbody2D body2D, float walkSpeed)
        {
            WalkSpeed = walkSpeed;
            _up = new(body2D, Vector2.up * WalkSpeed);
            _down = new(body2D, Vector2.down * WalkSpeed);
            _left = new(body2D, Vector2.left * WalkSpeed);
            _right = new(body2D, Vector2.right * WalkSpeed);
            _stop = new(body2D, Vector2.zero);
        }

        public MoveCommand GetMoveCommand()
        {
            return _moveCommands.Count > 0 ? _moveCommands[0] : _stop;
        }

        public void AddMoveCommand(Direction direction)
        {
            MoveCommand moveDirection = direction switch
            {
                Direction.Up => _up,
                Direction.Down => _down,
                Direction.Left => _left,
                Direction.Right => _right,
                _ => throw new System.NotImplementedException(),
            };
            RemoveMoveCommand(moveDirection);
            _moveCommands.Insert(0, moveDirection);
        }


        public void RemoveMoveCommand(Direction direction)
        {
            MoveCommand moveDirection = direction switch
            {
                Direction.Up => _up,
                Direction.Down => _down,
                Direction.Left => _left,
                Direction.Right => _right,
                _ => throw new System.NotImplementedException(),
            };
            if (_moveCommands.Contains(moveDirection)) { _moveCommands.Remove(moveDirection); }
        }

        private void RemoveMoveCommand(MoveCommand moveCommand)
        {
            if (_moveCommands.Contains(moveCommand)) { _moveCommands.Remove(moveCommand); }
        }
    }
}