using UnityEngine;
namespace Game.Features.Characters
{
    public partial class MoveCommand : ICommand
    {
        public Rigidbody2D Body2D { get; private set; }
        public Vector2 Velocity { get; private set; }

        public MoveCommand(Rigidbody2D body2D, Vector2 velocity)
        {
            Body2D = body2D;
            Velocity = velocity;
        }

        public void Execute()
        {
            Body2D.velocity = Velocity;
        }
    }

}
