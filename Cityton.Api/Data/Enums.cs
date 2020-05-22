using System;
namespace Cityton.Api.Data
{
    public enum Status
    {
        Waiting,
        Accepted
    }

    public enum StatusChallenge
    {
        InProgress,
        Waiting,
        Validated,
        Rejected
    }

    public enum Role
    {
        Member = 0,
        Checker = 1,
        Admin = 2
    }
    
    public enum FilterGroup
    {
        All = 0,
        Full = 1,
        NotFull = 2,
        InferiorToMinSize = 3

    }
}
