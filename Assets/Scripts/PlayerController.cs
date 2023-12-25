using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector3 mousePosition;


    [SerializeField]
    private GameObject playerWeaponPivot, shootOriginPoint, projectileObject;

    [SerializeField]
    private SpriteRenderer playerSprite;

    private PlayerStatusHandler statusHandler;

    private bool canShoot;

    #endregion

    #region MonoBehaviour
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        statusHandler = PlayerStatusHandler.Instance;
        canShoot = true;
    }

    private void Update()
    {
        ProcessInputs();
        WeaponFaceMouse();
        RotatePlayerFollowingMouseXPosition();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region Methods
    private void WeaponFaceMouse()
    {
        Vector2 direction = new Vector2(mousePosition.x - playerWeaponPivot.transform.position.x, mousePosition.y - playerWeaponPivot.transform.position.y);
        playerWeaponPivot.transform.up = direction;
    }

    private void RotatePlayerFollowingMouseXPosition()
    {
        if (mousePosition.x < transform.position.x)
        {
            playerSprite.flipX = true;
            playerWeaponPivot.transform.localPosition = new Vector2(-0.18f, 0.131f);
        }
        else
        {
            playerSprite.flipX = false;
            playerWeaponPivot.transform.localPosition = new Vector2(0.18f, 0.131f);
        }
    }

    private void Shoot()
    {
        if (!canShoot) return;

        if (Input.GetMouseButton(0))
        {
            StartCoroutine(ShootCoroutine());

            int criticalRandomValue = UnityEngine.Random.Range(0, 100);

            Vector2 direction = new Vector2(mousePosition.x - shootOriginPoint.transform.position.x, mousePosition.y - shootOriginPoint.transform.position.y);
            Projectile projectile = Instantiate(projectileObject, shootOriginPoint.transform.position, quaternion.identity).GetComponent<Projectile>();
            projectile.SetDir(direction);
            projectile.isCritical = criticalRandomValue <= statusHandler.statusValues.criticalChance;

        }
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * statusHandler.statusValues.movementSpeed, moveDirection.y * statusHandler.statusValues.movementSpeed);
    }

    private IEnumerator ShootCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(1 / statusHandler.statusValues.attackSpeed);
        canShoot = true;
    }

    #endregion
}
