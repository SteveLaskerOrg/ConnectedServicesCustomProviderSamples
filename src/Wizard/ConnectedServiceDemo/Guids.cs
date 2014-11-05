// Guids.cs
// MUST match guids.h
using System;

namespace Company.ConnectedServiceDemo
{
    static class GuidList
    {
        public const string guidConnectedServiceDemoPkgString = "8aeb5b01-9313-4073-bea7-8b40190025ed";
        public const string guidConnectedServiceDemoCmdSetString = "1f7373c6-331e-417b-97b1-9e740aa27f16";

        public static readonly Guid guidConnectedServiceDemoCmdSet = new Guid(guidConnectedServiceDemoCmdSetString);
    };
}