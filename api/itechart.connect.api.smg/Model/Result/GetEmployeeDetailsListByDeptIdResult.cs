using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class GetEmployeeDetailsListByDeptIdResult : BaseApiResult<GetEmployeeDetailsListOutput>
    {
        [DataMember(Name = "Profiles", EmitDefaultValue = false)]
        public List<EmployeeFullResult> Profiles { get; set; }

        public override void InitializeFromOutput(GetEmployeeDetailsListOutput output)
        {
            base.InitializeFromOutput(output);

            if (Profiles != null && Profiles.Any())
            {
                Profiles.Clear();
            }
            else
            {
                Profiles = new List<EmployeeFullResult>();
            }

            if (output.Profiles != null && output.Profiles.Any())
            {
                Profiles.AddRange(output.Profiles.Select(x =>
                {
                    var employee = new EmployeeFullResult();
                    employee.InitializeFromOutput(x);
                    return employee;
                }));
            }
        }
    }
}