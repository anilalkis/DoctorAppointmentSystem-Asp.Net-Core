namespace DoctorAppointmentSystem.WebUI.EmailServices
{
    public interface IEmailService
    {
        void Execute(string email, string subject, string message );
    }
}
