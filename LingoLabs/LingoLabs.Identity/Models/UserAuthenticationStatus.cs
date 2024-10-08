﻿namespace LingoLabs.Identity.Models
{
    public static class UserAuthenticationStatus
    {
        public const int REGISTRATION_SUCCES = 1;
        public const int REGISTRATION_FAIL = 0;
        public const int LOGIN_SUCCES = 1;
        public const int LOGIN_FAIL = 0;
        public const int LOGOUT_SUCCES = 1;
        public const int USER_NOT_FOUND = 2;
        public const int APPROVAL_FAIL = 3;
    }
}
