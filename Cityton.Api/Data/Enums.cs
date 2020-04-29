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
}
