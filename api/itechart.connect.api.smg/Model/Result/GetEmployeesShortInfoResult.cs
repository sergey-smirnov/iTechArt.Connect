using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using itechart.connect.api.smg.SmgMobileService;

namespace itechart.connect.api.smg.Model.Result
{
    [DataContract]
    public class GetEmployeesShortInfoResult : BaseApiResult<GetEmployeeShortOutput>
    {
        public List<EmployeeExtraShortResult> Profiles { get; set; }

        public override void InitializeFromOutput(GetEmployeeShortOutput output)
        {
            base.InitializeFromOutput(output);

            if (Profiles != null && Profiles.Any())
            {
                Profiles.Clear();
            }
            else
            {
                Profiles = new List<EmployeeExtraShortResult>();
            }

            if (output.Profiles != null && output.Profiles.Any())
            {
                Profiles.AddRange(output.Profiles.Select(x =>
                {
                    var employee = new EmployeeExtraShortResult();
                    employee.InitializeFromOutput(x);
                    return employee;
                }));
            }
        }
    }
}