using ReadService;
using AutoEntity.EntityModels;

namespace ModifyService
{
    public class DataModify
    {
        public DataModify()
        {

        }

        #region Plant

        public int SaveBusiness(Plant plant)
        {
            if (plant == null)
                throw new ArgumentNullException(nameof(plant));

            bool isExists = false;

            if (plant.PlantId > 0)
            {
                try
                {
                    QPrimaryService.GetPlant(plant.PlantId);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }

            int isSaved;

            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.Update(plant);
                }
                else
                {
                    ecomContext.Add(plant);
                }

                isSaved = ecomContext.SaveChanges();
            }

            if (isSaved > 0)
                return plant.PlantId;

            return 0;
        }

        #endregion

        #region Division
        public int SaveDivision(Division division)
        {
            if (division == null)
                throw new ArgumentException(nameof(Division));

            var isExists = false;
            if (division.DivisionId > 0)
            {
                try
                {
                    QPrimaryService.GetDivision(division.DivisionId);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(division);
                }
                else
                {
                    ecomContext.AttachRange(division);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = division.DivisionId;
            return bid;
        }
        #endregion

        #region Machine
        public int SaveMachine(Machine machine)
        {
            if (machine == null)
                throw new ArgumentException(nameof(Machine));

            var isExists = false;
            if (machine.MachineID > 0)
            {
                try
                {
                    QPrimaryService.GetMachine(machine.MachineID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(machine);
                }
                else
                {
                    ecomContext.AttachRange(machine);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = machine.MachineID;
            return bid;
        }
        #endregion

        #region Project
        public int SaveProject(Project project)
        {
            if (project == null)
                throw new ArgumentException(nameof(Project));

            var isExists = false;
            if (project.ProjectID > 0)
            {
                try
                {
                    QPrimaryService.GetProject(project.ProjectID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(project);
                }
                else
                {
                    ecomContext.AttachRange(project);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = project.ProjectID;
            return bid;
        }
        #endregion

        #region Components
        public int SaveComponents(Components components)
        {
            if (components == null)
                throw new ArgumentException(nameof(Components));

            var isExists = false;
            if (components.ComponentID > 0)
            {
                try
                {
                    QPrimaryService.GetComponents(components.ComponentID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(components);
                }
                else
                {
                    ecomContext.AttachRange(components);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = components.ComponentID;
            return bid;
        }
        #endregion

        #region Activities
        public int SaveActivities(Activities activities)
        {
            if (activities == null)
                throw new ArgumentException(nameof(Activities));

            var isExists = false;
            if (activities.ActivitiesID > 0)
            {
                try
                {
                    QPrimaryService.GetActivities(activities.ActivitiesID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(activities);
                }
                else
                {
                    ecomContext.AttachRange(activities);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = activities.ActivitiesID;
            return bid;
        }
        #endregion

        #region SubActivities
        public int SaveSubActivities(SubActivities subactivities)
        {
            if (subactivities == null)
                throw new ArgumentException(nameof(SubActivities));

            var isExists = false;
            if (subactivities.SubActivitiesID > 0)
            {
                try
                {
                    QPrimaryService.GetSubActivities(subactivities.SubActivitiesID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(subactivities);
                }
                else
                {
                    ecomContext.AttachRange(subactivities);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = subactivities.SubActivitiesID;
            return bid;
        }
        #endregion

        #region Shift
        public int SaveShift(Shift shift)
        {
            if (shift == null)
                throw new ArgumentException(nameof(Shift));

            var isExists = false;
            if (shift.ShiftID > 0)
            {
                try
                {
                    QPrimaryService.GetShift(shift.ShiftID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(shift);
                }
                else
                {
                    ecomContext.AttachRange(shift);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = shift.ShiftID;
            return bid;
        }
        #endregion

        #region Designation
        public int SaveDesignation(Designation designation)
        {
            if (designation == null)
                throw new ArgumentException(nameof(Designation));

            var isExists = false;
            if (designation.DesignationID > 0)
            {
                try
                {
                    QPrimaryService.GetDesignation(designation.DesignationID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(designation);
                }
                else
                {
                    ecomContext.AttachRange(designation);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = designation.DesignationID;
            return bid;
        }
        #endregion

        #region Employee
        public int SaveEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentException(nameof(Employee));

            var isExists = false;
            if (employee.EmployeeID > 0)
            {
                try
                {
                    QPrimaryService.GetEmployee(employee.EmployeeID);
                    isExists = true;
                }
                catch (Exception)
                {
                    isExists = false;
                }
            }
            int isSaved;
            using (var ecomContext = new MasterPorterContext())
            {
                if (isExists)
                {
                    ecomContext.UpdateRange(employee);
                }
                else
                {
                    ecomContext.AttachRange(employee);
                }
                isSaved = ecomContext.SaveChanges();
            }
            int bid = 0;
            if (isSaved == 1)
                bid = employee.EmployeeID;
            return bid;
        }
        #endregion
    }
}
