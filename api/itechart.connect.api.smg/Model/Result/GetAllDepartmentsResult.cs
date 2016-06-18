using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class GetAllDepartmentsResult : BaseApiResult<GetAllDepartmentsOutput>
    {
        [DataMember(Name = "Depts", EmitDefaultValue = false)]
        public List<DepartmentResult> Departments { get; set; }

        public override void InitializeFromOutput(GetAllDepartmentsOutput output)
        {
            base.InitializeFromOutput(output);

            if (Departments != null && Departments.Any())
            {
                Departments.Clear();
            }
            else
            {
                Departments = new List<DepartmentResult>();
            }

            if (output.Depts != null && output.Depts.Any())
            {
                Departments.AddRange(output.Depts.Select(x =>
                {
                    var department = new DepartmentResult();
                    department.InitializeFromOutput(x);
                    return department;
                }));
            }
        }
    }
}