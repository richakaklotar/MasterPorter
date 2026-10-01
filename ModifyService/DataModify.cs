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
        public int SavePlant(Plant plant)
        {
            if (string.IsNullOrWhiteSpace(plant.PlantName))
                throw new Exception("Plant Name is required.");

            if (string.IsNullOrWhiteSpace(plant.PlantCode))
                throw new Exception("Plant Code is required.");

            using var context = new MasterPorterContext();

            // Duplicate Plant Code check
            bool duplicate = context.Plant.Any(x =>
                x.PlantCode == plant.PlantCode && x.PlantId != plant.PlantId);

            if (duplicate)
                throw new Exception("Plant Code already exists.");

            if (plant.PlantId == 0)
            {
                if (string.IsNullOrWhiteSpace(plant.Status))
                    plant.Status = "Active";

                context.Plant.Add(plant);
            }
            else
            {
                var existing = context.Plant
                    .FirstOrDefault(x => x.PlantId == plant.PlantId);

                if (existing == null)
                    throw new Exception("Plant not found.");

                existing.PlantName = plant.PlantName;
                existing.PlantCode = plant.PlantCode;
                existing.Status = string.IsNullOrWhiteSpace(plant.Status)
                    ? "Active"
                    : plant.Status;
            }

            context.SaveChanges();
            return plant.PlantId == 0 ? 0 : plant.PlantId;
        }

        public bool DeletePlant(int id)
        {
            using var context = new MasterPorterContext();

            var plant = context.Plant.FirstOrDefault(x => x.PlantId == id);
            if (plant == null)
                return false;

            context.Plant.Remove(plant);
            context.SaveChanges();
            return true;
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
        public static int SaveMachine(Machine machine)
        {
            if (machine == null)
                throw new ArgumentException(nameof(machine));

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
                    ecomContext.Update(machine);
                }
                else
                {
                    ecomContext.Add(machine);
                }

                isSaved = ecomContext.SaveChanges();
            }

            if (isSaved > 0)
                return machine.MachineID;

            return 0;
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

        #region JobCard
        public int SaveJobCard(JobCard jobCard)
        {
            if (jobCard == null)
                throw new ArgumentException(nameof(JobCard));

            if (jobCard.Date == default)
                throw new Exception("Date is required.");

            if (jobCard.ShiftID <= 0)
                throw new Exception("Shift is required.");

            if (jobCard.ProjectID <= 0)
                throw new Exception("Project is required.");

            if (jobCard.ComponentID <= 0)
                throw new Exception("Component is required.");

            if (jobCard.ActivitiesID <= 0)
                throw new Exception("Activity is required.");

            if (jobCard.SubActivitiesID <= 0)
                throw new Exception("Sub Activity is required.");

            if (jobCard.TotalHours < 0 || jobCard.TotalHours > 24)
                throw new Exception("Total Hours must be between 0 and 24.");

            if (string.IsNullOrWhiteSpace(jobCard.Status))
                jobCard.Status = "Active";

            var isExists = false;

            if (jobCard.JobCardID > 0)
            {
                try
                {
                    QPrimaryService.GetJobCard(jobCard.JobCardID);
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
                    var existing = ecomContext.JobCard.FirstOrDefault(x => x.JobCardID == jobCard.JobCardID);

                    if (existing == null)
                        throw new Exception("Job Card not found.");

                    existing.Date = jobCard.Date;
                    existing.Operator = jobCard.Operator;
                    existing.ShiftID = jobCard.ShiftID;
                    existing.ProjectID = jobCard.ProjectID;
                    existing.ComponentID = jobCard.ComponentID;
                    existing.ActivitiesID = jobCard.ActivitiesID;
                    existing.SubActivitiesID = jobCard.SubActivitiesID;
                    existing.StartTime = jobCard.StartTime;
                    existing.EndTime = jobCard.EndTime;
                    existing.TotalHours = jobCard.TotalHours;
                    existing.Remarks = jobCard.Remarks;
                    existing.IsRework = jobCard.IsRework;
                    existing.Status = jobCard.Status;
                }
                else
                {
                    ecomContext.JobCard.Add(jobCard);
                }

                isSaved = ecomContext.SaveChanges();
            }

            if (isSaved > 0)
                return jobCard.JobCardID;

            return 0;
        }

        public bool DeleteJobCard(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid JobCardID.");

            using var context = new MasterPorterContext();

            var jobCard = context.JobCard.FirstOrDefault(x => x.JobCardID == id);

            if (jobCard == null)
                return false;

            context.JobCard.Remove(jobCard);
            context.SaveChanges();

            return true;
        }

        #endregion
    }
}
