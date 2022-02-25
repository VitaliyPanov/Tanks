using General.Services;
using UnityEngine;

namespace Tanks.Core.Infrastructure.Services
{
    internal class TimeService :  ITimeService
    {
        public float DeltaTime() => Time.deltaTime;
        public float FixedDeltaTime() => Time.fixedDeltaTime;
        public float RealtimeSinceStartup() => Time.realtimeSinceStartup;
    }
}