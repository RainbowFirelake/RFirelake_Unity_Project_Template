using System;
using UnityEngine;

namespace RFirelake.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class AutoAddAttribute : PropertyAttribute
    {
        
    }
}