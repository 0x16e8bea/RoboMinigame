﻿using UnityEngine;

namespace Content.Code.Gameplay.Robot.State
{
    public interface IRobotData
    {
        int Health { get; set; }
        bool IsGrounded { get; set; }
        bool CanShoot { get; set; }
        bool IsChangingLanes { get; set; }
        Vector3 Velocity { get; set; }
    }
}