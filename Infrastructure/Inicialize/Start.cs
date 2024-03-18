using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;

namespace Infrastructure.Inicialize;

public class Start
{
    private readonly PersistenceContext _context;

    public Start(PersistenceContext context)
    {
        _context = context;
    }

    public void Inicializar()
    {
        #region eps

        if (!_context.Eps.Any(u => u.State == EpsState.Active))
        {
            Eps[] defaultEpsArray = new Eps[]
            {
                new Eps("Salud Total"),
                new Eps("Nueva EPS"),
                new Eps("EPS Sura"),
                new Eps("Sanitas EPS"),
                new Eps("Coomeva EPS"),
                new Eps("Aliansalud EPS"),
                new Eps("Cajacopi EPS"),
                new Eps("Compensar EPS"),
                new Eps("SOS EPS"),
                new Eps("Cruz Blanca EPS")
            };

            foreach (var eps in defaultEpsArray)
            {
                _context.Eps.Add(eps);
            }

            _context.SaveChanges();
        }

        #endregion

        #region UserAdmin

        if (!_context.Users.Any(u => u.Role == Role.Admin))
        {
            var defaultAdmin = new Admin
            (
                "User",
                "Admin",
                "User",
                "Admin",
                TypeDocument.IdentificationCard,
                "123456789",
                "adminDefault@mail.com",
                "123456789",
                "Address",
                DateTime.Now.AddYears(-18)
            );

            var defaultUser = new User
            (
                "adminDefault",
                Role.Admin,
                defaultAdmin
            );
            _context.Users.Add(defaultUser);
            _context.SaveChanges();
        }

        #endregion

        #region UserDoctor

        if (!_context.Users.Any(u => u.Role == Role.Doctor))
        {
            var doctorsData =
                new List<(string firstName, string secondName, string lastName, string secondLastName, TypeDocument
                    documentType, string documentNumber, string email, string phone, string address, DateTime
                    birthdate, string specialization)>
                {
                    ("John", "A.", "Doe", "", TypeDocument.IdentificationCard, "123456789", "john.doe@example.com",
                        "987654321", "123 Main St", new DateTime(1980, 5, 15), "Odontología general"),
                    ("Alice", "B.", "Smith", "", TypeDocument.IdentificationCard, "234567890",
                        "alice.smith@example.com", "987654322", "456 Oak St", new DateTime(1982, 8, 21),
                        "Odontología general"),
                    ("Robert", "C.", "Johnson", "", TypeDocument.IdentificationCard, "345678901",
                        "robert.johnson@example.com", "987654323", "789 Pine St", new DateTime(1985, 11, 30),
                        "Odontología general"),
                    ("Emily", "D.", "Brown", "", TypeDocument.IdentificationCard, "456789012",
                        "emily.brown@example.com", "987654324", "567 Elm St", new DateTime(1978, 3, 7),
                        "Odontología general"),
                    ("David", "E.", "White", "", TypeDocument.IdentificationCard, "567890123",
                        "david.white@example.com", "987654325", "890 Cedar St", new DateTime(1989, 6, 12),
                        "Odontología general"),
                    ("Emma", "F.", "Taylor", "", TypeDocument.IdentificationCard, "678901234",
                        "emma.taylor@example.com", "987654326", "234 Birch St", new DateTime(1983, 9, 18),
                        "Odontología general"),
                    ("Michael", "G.", "Miller", "", TypeDocument.IdentificationCard, "789012345",
                        "michael.miller@example.com", "987654327", "678 Maple St", new DateTime(1981, 2, 25),
                        "Odontología general"),
                    ("Olivia", "H.", "Davis", "", TypeDocument.IdentificationCard, "890123456",
                        "olivia.davis@example.com", "987654328", "890 Walnut St", new DateTime(1987, 7, 3),
                        "Odontología general"),
                    ("Daniel", "I.", "Moore", "", TypeDocument.IdentificationCard, "901234567",
                        "daniel.moore@example.com", "987654329", "123 Pine St", new DateTime(1984, 10, 10),
                        "Odontología general"),
                    ("Sophia", "J.", "Clark", "", TypeDocument.IdentificationCard, "012345678",
                        "sophia.clark@example.com", "987654330", "456 Oak St", new DateTime(1986, 4, 28),
                        "Odontología general"),
                    ("María", "Isabel", "González", "López", TypeDocument.IdentificationCard, "123456788",
                        "mariagonzalez@example.com", "987654321", "Calle 123, Ciudad", new DateTime(1980, 5, 15),
                        "Odontología general")
                };

            foreach (var doctorData in doctorsData)
            {
                var doctor = new Doctor(
                    doctorData.firstName,
                    doctorData.secondName,
                    doctorData.lastName,
                    doctorData.secondLastName,
                    doctorData.documentType,
                    doctorData.documentNumber,
                    doctorData.email,
                    doctorData.phone,
                    doctorData.address,
                    doctorData.birthdate,
                    doctorData.specialization
                );

                var defaultUser = new User($"password{doctor.FirstName}", Role.Doctor, doctor);

                _context.Doctors.Add(doctor);
                _context.Users.Add(defaultUser);
            }

            _context.SaveChanges();
        }

        #endregion


        #region UserPatient

        if (!_context.Users.Any(u => u.Role == Role.Patient))
        {
            var patientsData =
                new List<(string firstName, string secondName, string lastName, string secondLastName, TypeDocument
                    documentType, string documentNumber, string email, string phone, string address, DateTime
                    birthdate)>
                {
                    ("Luis", "", "González", "Sánchez", TypeDocument.IdentificationCard, "234567890",
                        "luis.gonzalez@example.com", "987654321", "456 Oak St", new DateTime(1982, 8, 21)),
                    ("María", "", "Martínez", "Pérez", TypeDocument.IdentificationCard, "345678901",
                        "maria.martinez@example.com", "987654322", "789 Pine St", new DateTime(1985, 11, 30)),
                    ("Ana", "", "López", "Gómez", TypeDocument.IdentificationCard, "456789012",
                        "ana.lopez@example.com", "987654323", "567 Elm St", new DateTime(1978, 3, 7)),
                    ("Juan", "", "Hernández", "Rodríguez", TypeDocument.IdentificationCard, "567890123",
                        "juan.hernandez@example.com", "987654324", "890 Cedar St", new DateTime(1989, 6, 12)),
                    ("Laura", "", "Díaz", "Ramírez", TypeDocument.IdentificationCard, "678901234",
                        "laura.diaz@example.com", "987654325", "234 Birch St", new DateTime(1983, 9, 18)),
                    ("Carlos", "", "Sánchez", "Martínez", TypeDocument.IdentificationCard, "789012345",
                        "carlos.sanchez@example.com", "987654326", "678 Maple St", new DateTime(1981, 2, 25)),
                    ("Sofía", "", "Ramírez", "Sánchez", TypeDocument.IdentificationCard, "890123456",
                        "sofia.ramirez@example.com", "987654327", "890 Walnut St", new DateTime(1987, 7, 3)),
                    ("David", "", "Gómez", "López", TypeDocument.IdentificationCard, "901234567",
                        "david.gomez@example.com", "987654328", "123 Pine St", new DateTime(1984, 10, 10)),
                    ("Elena", "", "Pérez", "Martínez", TypeDocument.IdentificationCard, "012345678",
                        "elena.perez@example.com", "987654329", "456 Oak St", new DateTime(1986, 4, 28)),
                    ("Javier", "", "Rodríguez", "García", TypeDocument.IdentificationCard, "123456788",
                        "javier.rodriguez@example.com", "987654330", "Calle 123, Ciudad",
                        new DateTime(1980, 5, 15)),
                    ("Andrea", "", "García", "López", TypeDocument.IdentificationCard, "234567889",
                        "andrea.garcia@example.com", "987654331", "Calle 456, Ciudad", new DateTime(1975, 12, 20)),
                    ("Miguel", "", "Fernández", "Hernández", TypeDocument.IdentificationCard, "345678890",
                        "miguel.fernandez@example.com", "987654332", "Carrera 789, Ciudad",
                        new DateTime(1973, 10, 5)),
                    ("Paula", "", "Jiménez", "Martínez", TypeDocument.IdentificationCard, "456789011",
                        "paula.jimenez@example.com", "987654333", "Avenida 987, Ciudad", new DateTime(1990, 6, 25)),
                    ("Gabriel", "", "Ortiz", "Sánchez", TypeDocument.IdentificationCard, "567890122",
                        "gabriel.ortiz@example.com", "987654334", "Carrera 654, Ciudad", new DateTime(1977, 8, 14))
                };

            var epsList = _context.Eps.ToList();
            var random = new Random();

            foreach (var patientData in patientsData)
            {
                var randomEps = epsList[random.Next(epsList.Count)];

                var patient = new Patient
                (
                    patientData.firstName,
                    patientData.secondName,
                    patientData.lastName,
                    patientData.secondLastName,
                    patientData.documentType,
                    patientData.documentNumber,
                    patientData.email,
                    patientData.phone,
                    patientData.address,
                    patientData.birthdate,
                    randomEps.Id
                );

                var user = new User
                (
                    $"{patientData.firstName.ToLower()}{patientData.lastName.ToLower()}",
                    Role.Patient,
                    patient
                );

                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }

        #endregion

        #region Disease

        if (!_context.Diseases.Any(u => u.DeletedOn == null))
        {
            Disease[] defaultDiseaseArray = new Disease[]
            {
                new Disease("Halitosis"),
                new Disease("Caries"),
                new Disease("Extraccion dental"),
                new Disease("Gingivitis"),
                new Disease("Periodontitis"),
                new Disease("Edentulismo")
            };
            foreach (var disease in defaultDiseaseArray)
            {
                _context.Diseases.Add(disease);
            }

            _context.SaveChanges();
        }

        #endregion

        #region Appointment

        if (!_context.Appointments.Any())
        {
            var patientList = _context.Users.Where(u => u.Role == Role.Patient).ToList();
            var doctorList = _context.Doctors.ToList();

            var random = new Random();

            // Define el rango de horas (8 am - 5 pm)
            var startTime = TimeSpan.FromHours(8);
            var endTime = TimeSpan.FromHours(17);

            // Calcula la diferencia en minutos entre la hora de inicio y la hora de finalización
            var totalTimeInMinutes = (int)(endTime - startTime).TotalMinutes;
            for (int i = 0; i < 100; i++)
            {
                var randomPatient = patientList[random.Next(patientList.Count)];
                var randomDoctor = doctorList[random.Next(doctorList.Count)];

                var appointment = new Appointment(
                    new DateTime(2024, 04, 14),
                    TypeAppointment.General,
                    "dadfdsfdsf",
                    randomPatient.PersonId,
                    randomDoctor.Id
                );
                _context.Appointments.Add(appointment);
            }

            _context.SaveChanges();
        }

        #endregion
    }
}