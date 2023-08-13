using UnityEngine;

public class GameClass : MonoBehaviour, IInitializable
{
    protected GameManager GameManager;

    protected void Start()
    {
        AtStart();
    }

    protected void Update()
    {
        if (GameManager.StateManager.StateEnum.HasFlag(StateEnum.Game))
            AtUpdate();
    }

    protected void FixedUpdate()
    {
        if (GameManager.StateManager.StateEnum.HasFlag(StateEnum.Game))
            AtFixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AtOnTriggerEnter2D(other);
    }

    public void Init(GameManager gameManager)
    {
        GameManager = gameManager;
        AtInit();
    }

    protected virtual void AtStart()
    {
    }

    protected virtual void AtUpdate()
    {
    }

    protected virtual void AtFixedUpdate()
    {
    }

    protected virtual void AtInit()
    {
    }

    protected virtual void AtOnTriggerEnter2D(Collider2D other)
    {
    }
}