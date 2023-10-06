namespace Preschool.Models.ViewModels
{
    public class ChildAttendanceVM
    {

        public int Id { get; set; }
        public TimeOnly EnterTime { get; set; }

        public TimeOnly ExitTime { get; set; }  

        public DateTime Date {  get; set; }

        public int AttendanceHours()
        {
            return ExitTime.Hour - EnterTime.Hour;
        }
    }
}
