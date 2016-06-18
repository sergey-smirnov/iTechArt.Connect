using System;
using System.Runtime.Serialization;
using System.Security.Authentication;

namespace itechart.connect.api.smg.Model.Struct
{
    [DataContract]
    public struct ErrorCode : IEquatable<ErrorCode>
    {
        #region constants

        public const string NoneErrorCode = "";
        public const string IncorrectCredentialsErrorCode = "Incorrect user name or password";
        public const string AnotherUserLoggedErrorCode = "There is no session for the user given. Another User was logged in using this account from the other IP";

        public const string TimeoutExpiredErrorCode =
            "Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.";

        #endregion

        #region members

        #region properties

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public int Value { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        #endregion
        
        private ErrorCode(int value, string name)
            : this()
        {
            Name = name;
            Value = value;
        }


        public bool Equals(ErrorCode other)
        {
            return Value == other.Value && Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public override string ToString()
        {
            return Name;
        }

        public void Validate()
        {
            if (Equals(None))
            {
                return;
            }

            if (Equals(AnotherUserLogged))
            {
                throw new AuthenticationException(Name);
            }
            if (Equals(IncorrectCredentials))
            {
                throw new InvalidCredentialException(Name);
            }
            if (Equals(TimeoutExpired))
            {
                throw new TimeoutException(Name);
            }

            throw new ApplicationException(Name);
        }

        #endregion

        #region static

        public static readonly ErrorCode None = new ErrorCode(0, NoneErrorCode);
        public static readonly ErrorCode IncorrectCredentials = new ErrorCode(1, IncorrectCredentialsErrorCode);
        public static readonly ErrorCode AnotherUserLogged = new ErrorCode(2, AnotherUserLoggedErrorCode);
        public static readonly ErrorCode TimeoutExpired = new ErrorCode(3, TimeoutExpiredErrorCode);

        public static ErrorCode FromString(string errorCode)
        {
            switch (errorCode)
            {
                case TimeoutExpiredErrorCode:
                    return TimeoutExpired;
                case AnotherUserLoggedErrorCode:
                    return AnotherUserLogged;
                case IncorrectCredentialsErrorCode:
                    return IncorrectCredentials;
                default:
                    return None;
            }
        }

        #endregion
    }
}