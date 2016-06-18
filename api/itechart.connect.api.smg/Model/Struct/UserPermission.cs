using System.Runtime.Serialization;

namespace itechart.connect.api.smg.Model.Struct
{
    [DataContract]
    public struct UserPermission
    {
        #region constants

        public const string NoneUserPermission = "";
        public const string ReadUserPermission = "READ";

        #endregion

        #region members

        #region properties

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public int Value { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        #endregion

        private UserPermission(int value, string name)
            : this()
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region static

        public static readonly UserPermission None = new UserPermission(0, NoneUserPermission);
        public static readonly UserPermission Read = new UserPermission(1, ReadUserPermission);

        public static UserPermission FromString(string errorCode)
        {
            switch (errorCode)
            {
                case ReadUserPermission:
                    return Read;
                default:
                    return None;
            }
        }

        #endregion
    }
}