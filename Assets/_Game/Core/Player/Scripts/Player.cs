using UnityEngine;
using Game.Features.Characters;
using System;
using System.Collections;

namespace Game.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public partial class Player : Character
	{
		[Header("Player Parameters")]
		[SerializeField] private float _walkSpeed = 3f;
		[SerializeField] private Transform _interactionPivot;
		[SerializeField] private float _interactionMaxDistance = 0.5f;
		[SerializeField] private LayerMask _interactionMask;

		// [SerializeField] public Inventory Inventory { get; private set; }

		private bool _collided;
		private bool _running;
		private bool _stopInput;

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

		private void Awake()
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

		private void Update()
		{
			ProcessInput();
			UpdateAnimationParameters();
		}

		private void FixedUpdate()
		{
			MoveCharacter();
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

		public IEnumerator StopInputForSeconds(float seconds)
		{
			_stopInput = true;
			_characterMovement.RemoveAllCommands();
			yield return new WaitForSeconds(seconds);
			_stopInput = false;
		}

		private void ProcessInput()
		{
			if (_stopInput) { return; }
			if (Input.GetButtonDown(InputActions.Interact)) { TryInteract(); }

			if (Input.GetButtonDown(InputActions.MoveUp)) { _characterMovement.AddMoveCommand(Direction.Up); }
			else if (Input.GetButtonUp(InputActions.MoveUp)) { _characterMovement.RemoveMoveCommand(Direction.Up); }

			if (Input.GetButtonDown(InputActions.MoveDown)) { _characterMovement.AddMoveCommand(Direction.Down); }
			else if (Input.GetButtonUp(InputActions.MoveDown)) { _characterMovement.RemoveMoveCommand(Direction.Down); }

			if (Input.GetButtonDown(InputActions.MoveLeft)) { _characterMovement.AddMoveCommand(Direction.Left); }
			else if (Input.GetButtonUp(InputActions.MoveLeft)) { _characterMovement.RemoveMoveCommand(Direction.Left); }

			if (Input.GetButtonDown(InputActions.MoveRight)) { _characterMovement.AddMoveCommand(Direction.Right); }
			else if (Input.GetButtonUp(InputActions.MoveRight)) { _characterMovement.RemoveMoveCommand(Direction.Right); }
		}

		private void MoveCharacter()
		{
			MoveCommand moveCommand;

			if (_stopInput) { moveCommand = _characterMovement.GetStopCommand(); }
			else { moveCommand = _characterMovement.GetMoveCommand(); }

			Vector2 velocity = moveCommand.Velocity;
			if (velocity != Vector2.zero) { FacingDirection = GetDirectionFromVector2(velocity); }
			moveCommand.Execute();
		}


		private void UpdateAnimationParameters()
		{
			Vector2 velocity = _rigidbody.velocity;

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
		}

		private bool TryInteract()
		{
			RaycastHit2D hit = Physics2D.Raycast(
				_interactionPivot.position,
				GetVector2FromDirection(FacingDirection),
				_interactionMaxDistance,
				_interactionMask.value
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

		private Direction GetDirectionFromVector2(Vector2 vector2)
		{
			Direction direction = Direction.Down;
			if (vector2.x > 0.1f) { direction = Direction.Right; }
			else if (vector2.x < -0.1f) { direction = Direction.Left; }
			else if (vector2.y > 0.1f) { direction = Direction.Up; }
			else if (vector2.y < -0.1f) { direction = Direction.Down; }
			return direction;
		}


	}

}