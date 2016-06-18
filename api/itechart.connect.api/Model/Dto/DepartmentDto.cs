using System;
using System.Runtime.Serialization;
using itechart.connect.api.Helpers;
using itechart.connect.api.Model.Extended;
using itechart.connect.api.Services;
using itechart.PerformanceReview.Data.Model.Enum;

namespace itechart.connect.api.Model.Dto
{
    [DataContract]
    public class DepartmentDto
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "employeesCount")]
        public int EmployeesCount { get; set; }

        [DataMember(Name = "isLocked")]
        public bool IsLocked { get; set; }

        [DataMember(Name = "employeesAvailable")]
        public bool EmployeesAvailable { get; set; }

        [DataMember(Name = "departmentManager")]
        public UserDto DepartmentManager { get; set; }

        public static DepartmentDto CreateFromModel(DepartmentExtended model)
        {
            if (model == null)
            {
                return null;
            }

            var departmentDto = new DepartmentDto
            {
                Code = model.DepartmentCode,
                Name = model.Name,
                EmployeesCount = model.Users.Count,
                IsLocked = model.IsLocked,
                EmployeesAvailable = true
            };

            if (model.DepartmentManager != null)
            {
                departmentDto.DepartmentManager = UserDto.CreateFromModel(model.DepartmentManager);
            }

            if (ApplicationSettingsService.Instance.Settings.EmployeesAccessRule != (int)EmployeesAccessRules.AllEmployees)
            {
                var currentDepartmentId = SessionHelper.GetUserDepartmentIdFromOwinContext();
                if (currentDepartmentId != Guid.Empty && currentDepartmentId != model.DepartmentId)
                {
                    departmentDto.EmployeesAvailable = false;
                }
            }

            return departmentDto;
        }
    }
}