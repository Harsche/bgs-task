using UnityEngine;
using Game.Features.Characters;
using System;

namespace Game.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public partial class Player : MonoBehaviour
	{
		[SerializeField] private float _walkSpeed = 3f;
		[SerializeField] private Animator _animator;
		[SerializeField] private Transform _interactionPivot;
		[SerializeField] private float _interactionMaxDistance = 0.5f;
		// [SerializeField] public CollisionShape2D HorizontalCollider { get; private set; }
		// [SerializeField] public CollisionShape2D VerticalCollider { get; private set; }
		// [SerializeField] public RayCast2D InteractionRaycast { get; private set; }
		// [SerializeField] public Vector2 RaycastOffset { get; private set; }
		// [SerializeField] public Inventory Inventory { get; private set; }

		private bool _collided;
		private bool _running;
		private Rigidbody2D _rigidbody;

		private CharacterMovement _characterMovement;
		public static Player Instance { get; set; }
		public static Direction FacingDirection { get; private set; } = Direction.Down;

#if UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			Instance = null;
			FacingDirection = Direction.Down;
		}

#endif


		public void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;

			_rigidbody = GetComponent<Rigidbody2D>();

			_characterMovement = new(_rigidbody, _walkSpeed);
		}

		public void Update()
		{
			ProcessInput();
			UpdateAnimationParameters();
		}

		public void FixedUpdate()
		{
			_characterMovement.GetMoveCommand().Execute();
			// if (_running) { Velocity *= 2; }
			// AnimationPlayer.SpeedScale = _running ? 2 : 1;

			// if (Dialog.IsActive) { Velocity = Vector2.Zero; }

			// if (!Cutscene.IsPlayingCutscene) { SetAnimationState(_collided); }

			// InteractionRaycast.TargetPosition = FacingDirection switch
			// {
			// 	Direction.Up => Vector2.Up * RaycastOffset.Y,
			// 	Direction.Down => Vector2.Down * RaycastOffset.Y,
			// 	Direction.Left => Vector2.Left * RaycastOffset.X,
			// 	Direction.Right => Vector2.Right * RaycastOffset.X,
			// 	_ => throw new System.NotImplementedException(),
			// };

			// // "MoveAndSlide" already takes delta time into account.
			// var collision = MoveAndCollide(Velocity * (float)delta);

			// if (collision != null)
			// {
			// 	Vector2 normal = collision.GetNormal();
			// 	_collided = normal.Dot(Velocity.Normalized()) < 0;
			// }
			// else { _collided = false; }
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.black;
			if (Application.isPlaying && _interactionPivot != null)
			{
				Vector2 direction = GetVector2FromDirection(FacingDirection);

				Gizmos.DrawRay(_interactionPivot.position, direction * _interactionMaxDistance);
			}

		}

		private void ProcessInput()
		{
			if (Input.GetButtonDown(InputButtons.Interact)) { TryInteract(); }

			// _running = Input.IsActionPressed("run");

			if (Input.GetButtonDown(InputButtons.MoveUp)) { _characterMovement.AddMoveCommand(Direction.Up); }
			else if (Input.GetButtonUp(InputButtons.MoveUp)) { _characterMovement.RemoveMoveCommand(Direction.Up); }

			if (Input.GetButtonDown(InputButtons.MoveDown)) { _characterMovement.AddMoveCommand(Direction.Down); }
			else if (Input.GetButtonUp(InputButtons.MoveDown)) { _characterMovement.RemoveMoveCommand(Direction.Down); }

			if (Input.GetButtonDown(InputButtons.MoveLeft)) { _characterMovement.AddMoveCommand(Direction.Left); }
			else if (Input.GetButtonUp(InputButtons.MoveLeft)) { _characterMovement.RemoveMoveCommand(Direction.Left); }

			if (Input.GetButtonDown(InputButtons.MoveRight)) { _characterMovement.AddMoveCommand(Direction.Right); }
			else if (Input.GetButtonUp(InputButtons.MoveRight)) { _characterMovement.RemoveMoveCommand(Direction.Right); }
		}


		private void UpdateAnimationParameters()
		{
			Vector2 velocity = _rigidbody.velocity;
			// string state = Velocity.LengthSquared() > 1f && !collided ? "Walk_" : "Idle_";

			if (velocity.x > 0) { FacingDirection = Direction.Right; }
			else if (velocity.x < 0) { FacingDirection = Direction.Left; }
			else if (velocity.y > 0) { FacingDirection = Direction.Up; }
			else if (velocity.y < 0) { FacingDirection = Direction.Down; }

			if (FacingDirection is Direction.Up or Direction.Down)
			{
				_animator.SetFloat(PlayerAnimatorParameters.DirectionY, FacingDirection == Direction.Up ? 1 : -1);
				_animator.SetFloat(PlayerAnimatorParameters.DirectionX, 0);
			}
			else
			{
				_animator.SetFloat(PlayerAnimatorParameters.DirectionX, FacingDirection == Direction.Right ? 1 : -1);
				_animator.SetFloat(PlayerAnimatorParameters.DirectionY, 0);
			}

			_animator.SetFloat(PlayerAnimatorParameters.Speed, velocity.magnitude);

			// HorizontalCollider.Disabled = Mathf.Abs(Velocity.X) <= 0.001f;
			// VerticalCollider.Disabled = Mathf.Abs(Velocity.Y) <= 0.001f;

			// AnimationPlayer.CurrentAnimation = state + FacingDirection.ToString();
		}

		private bool TryInteract()
		{
			RaycastHit2D hit = Physics2D.Raycast(
				_interactionPivot.position,
				GetVector2FromDirection(FacingDirection),
				_interactionMaxDistance
			);

			Collider2D target = hit.collider;
			if (target == null || !target.CompareTag("Interaction")) { return false; }

			if (!target.TryGetComponent<Interaction>(out Interaction interaction))
			{
				Debug.LogWarning("Missing Interaction Script", target);
			}

			interaction.Interact();
			return true;
		}

		private Vector2 GetVector2FromDirection(Direction direction)
		{
			return direction switch
			{
				Direction.Up => Vector2.up,
				Direction.Down => Vector2.down,
				Direction.Left => Vector2.left,
				Direction.Right => Vector2.right,
				_ => throw new System.NotImplementedException(),
			};
		}


	}

}