using System.Collections.Generic;

namespace itechart.connect.api.Model.Binding
{
    public class UsersInRoleModel
    {
        public string Id { get; set; }

        public List<string> EnrolledUsers { get; set; }

        public List<string> RemovedUsers { get; set; }
    }
}