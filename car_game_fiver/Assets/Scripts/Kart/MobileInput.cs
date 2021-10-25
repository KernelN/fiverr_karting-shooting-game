using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class MobileInput : BaseInput
    {
        [SerializeField] float turn = 0;
        [SerializeField] bool brake = false;
        [SerializeField] bool accelerate = false;

        public void Turn(float x)
        {
            turn = x;
        }
        public void Accelerate()
        {
            accelerate = true;
            brake = false;
        }
        public void Brake()
        {
            accelerate = false;
            brake = true;
        }
        public void StopInputs()
        {
            accelerate = false;
            brake = false;
        }
        public override InputData GenerateInput()
        {
            return new InputData
            {
                Accelerate = accelerate,
                Brake = brake,
                TurnInput = turn
            };
        }
    }
}