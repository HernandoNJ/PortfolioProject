using UnityEngine;

public class PlayerTopDown : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private Vector2 direction;
	[SerializeField] private Rigidbody2D rb2D;

	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		PlayerMoveUpdate();
	}

	private void FixedUpdate()
	{
		PlayerMoveFixedUpate();
	}

	private void PlayerMoveUpdate()
	{
		var moveV = Input.GetAxisRaw("Vertical");
		var moveH = Input.GetAxisRaw("Horizontal");

		direction = new Vector2(moveH, moveV).normalized;
	}

	private void PlayerMoveFixedUpate()
	{
		// rb2D.position + direction: where the object is moving towards to
		rb2D.MovePosition(rb2D.position + direction * moveSpeed * Time.fixedDeltaTime);
	}

}
