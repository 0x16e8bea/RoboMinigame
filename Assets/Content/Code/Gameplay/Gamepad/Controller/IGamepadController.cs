namespace Content.Code.Gameplay.Gamepad.Controller
{
    public interface IGamepadController
    {
        void ApplyForceToDPad();
        void PressLeftButton();
        void PressRightButton();
        void PressAButton();
        void PressBButton();
        void ApplyForceToAButton();
        void ApplyForceToBButton();
    }
}