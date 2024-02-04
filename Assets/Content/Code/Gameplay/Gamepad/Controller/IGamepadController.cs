namespace Content.Code.Gameplay.Gamepad
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