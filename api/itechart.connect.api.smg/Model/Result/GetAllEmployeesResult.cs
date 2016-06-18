using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class GetAllEmployeesResult : BaseApiResult<GetEmployeesListOutput>
    {
        [DataMember(Name = "Profiles", EmitDefaultValue = false)]
        public List<EmployeeShortResult> Profiles { get; set; }

        public override void InitializeFromOutput(GetEmployeesListOutput output)
        {
            base.InitializeFromOutput(output);

            if (Profiles != null && Profiles.Any())
            {
                Profiles.Clear();
            }
            else
            {
                Profiles = new List<EmployeeShortResult>();
            }

            if (output.Profiles != null && output.Profiles.Any())
            {
                Profiles.AddRange(output.Profiles.Select(x =>
                {
                    var employee = new EmployeeShortResult();
                    employee.InitializeFromOutput(x);
                    return employee;
                }));
            }
        }
    }
}