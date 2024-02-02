public class RobotController : IRobotController, IUpdate, IFixedUpdate
{
    public bool CanUpdate => true;
    public bool CanFixedUpdate => true;

    private readonly RobotControllerSettings _settings;

    public RobotController(
        RobotControllerSettings settings,
        IMonoHookManager monoHookManager)
    {
        _settings = settings;
        monoHookManager.AddUpdateListener(this);
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }


    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }
}