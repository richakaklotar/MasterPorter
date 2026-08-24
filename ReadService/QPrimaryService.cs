using AutoEntity.EntityModels;

namespace ReadService
{
    public class QPrimaryService
    {
        #region Plant
        public static Plant GetPlant(int bid)
        {
            if (bid <= 0)
                throw new ArgumentNullException(nameof(bid));

            Plant plant;
            using (var ecomContext = new MasterPorterContext())
            {
                plant = ecomContext.Plants.Where(bus => bus.PlantId == bid).FirstOrDefault();
            }
            if (plant == null)
                throw new Exception("No data found with the provided data!");
            return plant;
        }

        public static string GetPlantName(int bid)
        {
            if (bid <= 0)
                throw new ArgumentNullException(nameof(bid));

            string plantName;
            using (var ecomContext = new MasterPorterContext())
            {
                plantName = ecomContext.Plants.Where(bus => bus.PlantId == bid).Select(b => b.PlantName).FirstOrDefault();
            }
            if (plantName == null)
                throw new Exception("No data found with the provided data!");
            return plantName;
        }

        /*Seperate hosting, So user specific records are not required*/
        public static List<Plant> GetPlantList()
        {
            List<Plant> plantList;
            using (var ecomContext = new MasterPorterContext())
            {
                plantList = ecomContext.Plants.Where(bus => bus.Isactive == true).ToList();
            }
            if (plantList == null || plantList.Count <= 0)
                throw new Exception("No data found with the provided data!");
            return plantList;
        }

        public static List<Plant> GetExistingPlantList()
        {
            List<Plant> plantList;
            using (var ecomContext = new MasterPorterContext())
            {
                plantList = ecomContext.Plants.Where(bus => bus.Isactive == true).ToList();
            }
            return plantList;
        }

        #endregion

        #region Division

        public static Division GetDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentNullException(nameof(divisionId));

            Division division;
            using (var ecomContext = new MasterPorterContext())
            {
                division = ecomContext.Division.FirstOrDefault(d => d.DivisionId == divisionId);
            }

            if (division == null)
                throw new Exception("No data found with the provided data!");

            return division;
        }

        public static string GetDivisionName(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentNullException(nameof(divisionId));

            string divisionName;
            using (var ecomContext = new MasterPorterContext())
            {
                divisionName = ecomContext.Division.Where(d => d.DivisionId == divisionId).Select(d => d.DivisionName).FirstOrDefault();
            }

            if (divisionName == null)
                throw new Exception("No data found with the provided data!");

            return divisionName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Division> GetDivisionList()
        {
            List<Division> divisionList;
            using (var ecomContext = new MasterPorterContext())
            {
                divisionList = ecomContext.Division.ToList();
            }

            if (divisionList == null || divisionList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return divisionList;
        }

        public static List<Division> GetExistingDivisionList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Division.ToList();
            }
        }
        #endregion

        #region Machine

        public static Machine GetMachine(int machineId)
        {
            if (machineId <= 0)
                throw new ArgumentNullException(nameof(machineId));

            Machine machine;
            using (var ecomContext = new MasterPorterContext())
            {
                machine = ecomContext.Machine.FirstOrDefault(d => d.MachineID == machineId);
            }

            if (machine == null)
                throw new Exception("No data found with the provided data!");

            return machine;
        }

        public static string GetMachineName(int machineId)
        {
            if (machineId <= 0)
                throw new ArgumentNullException(nameof(machineId));

            string machineName;
            using (var ecomContext = new MasterPorterContext())
            {
                machineName = ecomContext.Machine.Where(d => d.MachineID == machineId).Select(d => d.MachineName).FirstOrDefault();
            }

            if (machineName == null)
                throw new Exception("No data found with the provided data!");

            return machineName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Machine> GetMachineList()
        {
            List<Machine> machineList;
            using (var ecomContext = new MasterPorterContext())
            {
                machineList = ecomContext.Machine.ToList();
            }

            if (machineList == null || machineList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return machineList;
        }

        public static List<Machine> GetExistingMachineList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Machine.ToList();
            }
        }
        #endregion

        #region Project

        public static Project GetProject(int projectId)
        {
            if (projectId <= 0)
                throw new ArgumentNullException(nameof(projectId));

            Project project;
            using (var ecomContext = new MasterPorterContext())
            {
                project = ecomContext.Project.FirstOrDefault(d => d.ProjectID == projectId);
            }

            if (project == null)
                throw new Exception("No data found with the provided data!");

            return project;
        }

        public static string GetProjectName(int projectId)
        {
            if (projectId <= 0)
                throw new ArgumentNullException(nameof(projectId));

            string projectName;
            using (var ecomContext = new MasterPorterContext())
            {
                projectName = ecomContext.Project.Where(d => d.ProjectID == projectId).Select(d => d.ProjectName).FirstOrDefault();
            }

            if (projectName == null)
                throw new Exception("No data found with the provided data!");

            return projectName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Project> GetProjectList()
        {
            List<Project> projectList;
            using (var ecomContext = new MasterPorterContext())
            {
                projectList = ecomContext.Project.ToList();
            }

            if (projectList == null || projectList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return projectList;
        }

        public static List<Project> GetExistingProjectList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Project.ToList();
            }
        }
        #endregion

        #region Components

        public static Components GetComponents(int componentsId)
        {
            if (componentsId <= 0)
                throw new ArgumentNullException(nameof(componentsId));

            Components components;
            using (var ecomContext = new MasterPorterContext())
            {
                components = ecomContext.Components.FirstOrDefault(d => d.ComponentID == componentsId);
            }

            if (components == null)
                throw new Exception("No data found with the provided data!");

            return components;
        }

        public static string GetComponentsName(int componentsId)
        {
            if (componentsId <= 0)
                throw new ArgumentNullException(nameof(componentsId));

            string componentsName;
            using (var ecomContext = new MasterPorterContext())
            {
                componentsName = ecomContext.Components.Where(d => d.ComponentID == componentsId).Select(d => d.ComponentName).FirstOrDefault();
            }

            if (componentsName == null)
                throw new Exception("No data found with the provided data!");

            return componentsName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Components> GetComponentsList()
        {
            List<Components> componentsList;
            using (var ecomContext = new MasterPorterContext())
            {
                componentsList = ecomContext.Components.ToList();
            }

            if (componentsList == null || componentsList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return componentsList;
        }

        public static List<Components> GetExistingComponentsList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Components.ToList();
            }
        }
        #endregion

        #region Activities

        public static Activities GetActivities(int activitiesId)
        {
            if (activitiesId <= 0)
                throw new ArgumentNullException(nameof(activitiesId));

            Activities activities;
            using (var ecomContext = new MasterPorterContext())
            {
                activities = ecomContext.Activities.FirstOrDefault(d => d.ActivitiesID == activitiesId);
            }

            if (activities == null)
                throw new Exception("No data found with the provided data!");

            return activities;
        }

        public static string GetActivitiesName(int activitiesId)
        {
            if (activitiesId <= 0)
                throw new ArgumentNullException(nameof(activitiesId));

            string activitiesName;
            using (var ecomContext = new MasterPorterContext())
            {
                activitiesName = ecomContext.Activities.Where(d => d.ActivitiesID == activitiesId).Select(d => d.ActivitiesName).FirstOrDefault();
            }

            if (activitiesName == null)
                throw new Exception("No data found with the provided data!");

            return activitiesName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Activities> GetActivitiesList()
        {
            List<Activities> activitiesList;
            using (var ecomContext = new MasterPorterContext())
            {
                activitiesList = ecomContext.Activities.ToList();
            }

            if (activitiesList == null || activitiesList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return activitiesList;
        }

        public static List<Activities> GetExistingActivitiesList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Activities.ToList();
            }
        }
        #endregion

        #region SubActivities

        public static SubActivities GetSubActivities(int subactivitiesId)
        {
            if (subactivitiesId <= 0)
                throw new ArgumentNullException(nameof(subactivitiesId));

            SubActivities subactivities;
            using (var ecomContext = new MasterPorterContext())
            {
                subactivities = ecomContext.SubActivities.FirstOrDefault(d => d.SubActivitiesID == subactivitiesId);
            }

            if (subactivities == null)
                throw new Exception("No data found with the provided data!");

            return subactivities;
        }

        public static string GetSubActivitiesName(int subactivitiesId)
        {
            if (subactivitiesId <= 0)
                throw new ArgumentNullException(nameof(subactivitiesId));

            string subactivitiesName;
            using (var ecomContext = new MasterPorterContext())
            {
                subactivitiesName = ecomContext.SubActivities.Where(d => d.SubActivitiesID == subactivitiesId).Select(d => d.SubActivitiesName).FirstOrDefault();
            }

            if (subactivitiesName == null)
                throw new Exception("No data found with the provided data!");

            return subactivitiesName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<SubActivities> GetSubActivitiesList()
        {
            List<SubActivities> subactivitiesList;
            using (var ecomContext = new MasterPorterContext())
            {
                subactivitiesList = ecomContext.SubActivities.ToList();
            }

            if (subactivitiesList == null || subactivitiesList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return subactivitiesList;
        }

        public static List<SubActivities> GetExistingSubActivitiesList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.SubActivities.ToList();
            }
        }
        #endregion

        #region Shift

        public static Shift GetShift(int shiftId)
        {
            if (shiftId <= 0)
                throw new ArgumentNullException(nameof(shiftId));

            Shift shift;
            using (var ecomContext = new MasterPorterContext())
            {
                shift = ecomContext.Shift.FirstOrDefault(d => d.ShiftID == shiftId);
            }

            if (shift == null)
                throw new Exception("No data found with the provided data!");

            return shift;
        }

        public static string GetShiftName(int shiftId)
        {
            if (shiftId <= 0)
                throw new ArgumentNullException(nameof(shiftId));

            string shiftName;
            using (var ecomContext = new MasterPorterContext())
            {
                shiftName = ecomContext.Shift.Where(d => d.ShiftID == shiftId).Select(d => d.ShiftName).FirstOrDefault();
            }

            if (shiftName == null)
                throw new Exception("No data found with the provided data!");

            return shiftName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Shift> GetShiftList()
        {
            List<Shift> shiftList;
            using (var ecomContext = new MasterPorterContext())
            {
                shiftList = ecomContext.Shift.ToList();
            }

            if (shiftList == null || shiftList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return shiftList;
        }

        public static List<Shift> GetExistingShiftList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Shift.ToList();
            }
        }
        #endregion

        #region Designation

        public static Designation GetDesignation(int designationId)
        {
            if (designationId <= 0)
                throw new ArgumentNullException(nameof(designationId));

            Designation designation;
            using (var ecomContext = new MasterPorterContext())
            {
                designation = ecomContext.Designation.FirstOrDefault(d => d.DesignationID == designationId);
            }

            if (designation == null)
                throw new Exception("No data found with the provided data!");

            return designation;
        }

        public static string GetDesignationName(int designationId)
        {
            if (designationId <= 0)
                throw new ArgumentNullException(nameof(designationId));

            string designationName;
            using (var ecomContext = new MasterPorterContext())
            {
                designationName = ecomContext.Designation.Where(d => d.DesignationID == designationId).Select(d => d.DesignationName).FirstOrDefault();
            }

            if (designationName == null)
                throw new Exception("No data found with the provided data!");

            return designationName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Designation> GetDesignationList()
        {
            List<Designation> designationList;
            using (var ecomContext = new MasterPorterContext())
            {
                designationList = ecomContext.Designation.ToList();
            }

            if (designationList == null || designationList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return designationList;
        }

        public static List<Designation> GetExistingDesignationList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Designation.ToList();
            }
        }
        #endregion

        #region Employee

        public static Employee GetEmployee(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentNullException(nameof(employeeId));

            Employee employee;
            using (var ecomContext = new MasterPorterContext())
            {
                employee = ecomContext.Employee.FirstOrDefault(d => d.EmployeeID == employeeId);
            }

            if (employee == null)
                throw new Exception("No data found with the provided data!");

            return employee;
        }

        public static string GetEmployeeName(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentNullException(nameof(employeeId));

            string employeeName;
            using (var ecomContext = new MasterPorterContext())
            {
                employeeName = ecomContext.Employee.Where(d => d.EmployeeID == employeeId).Select(d => d.EmployeeName).FirstOrDefault();
            }

            if (employeeName == null)
                throw new Exception("No data found with the provided data!");

            return employeeName;
        }

        /* Separate hosting, so user specific records are not required */
        public static List<Employee> GetEmployeeList()
        {
            List<Employee> employeeList;
            using (var ecomContext = new MasterPorterContext())
            {
                employeeList = ecomContext.Employee.ToList();
            }

            if (employeeList == null || employeeList.Count == 0)
                throw new Exception("No data found with the provided data!");

            return employeeList;
        }

        public static List<Employee> GetExistingEmployeeList()
        {
            using (var ecomContext = new MasterPorterContext())
            {
                return ecomContext.Employee.ToList();
            }
        }
        #endregion
    }
}
