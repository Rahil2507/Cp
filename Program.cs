using CleanNShine.DAL.Models;
using CleanNShine.DAL;

namespace CleanNShine.ConsoleApp
{
    public class Program
    {
        static CleanNshineDbContext context;
        static CleanNShineRepository repository;

        static Program()
        {
            context = new CleanNshineDbContext();
            repository = new CleanNShineRepository(context);
        }

        static void Main(string[] args)
        {
            TestGetServices();
            //TestGetBookings();
            //TestGetFeedbacks();
            //TestGetUserDetails();
            //TestAddUser();
            //TestAddService();
            //TestAddBooking();
            //TestAddAddress();
            //TestAddPayment();
            //TestAddFeedback();
            //TestUpdateService();
            //TestRemoveUser();
            //TestRemoveService();
            //TestRemoveBooking();
            //TestRemoveFeedback();
        }

        #region TestAddUser
        public static void TestAddUser()
        {
            var user = new User
            {
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice.johnson@example.com",
                Contact = 5678901234,
                Password = "alicepass",
                DateOfBirth = new DateOnly(1991, 6, 25),
                Gender = "Female",
                Role = "Customer"
            };

            bool result = repository.AddUser(user);
            Console.WriteLine(result ? "User added successfully." : "Failed to add user.");
        }
        #endregion

        #region TestAddService
        public static void TestAddService()
        {
            var service = new Service
            {
                ServiceType = "Indoor",
                ServiceName = "Sofa Cleaning",
                Description = "Deep sofa cleaning service",
                Duration = 2,
                SuppliesRequired = "Vacuum, Brush",
                ServiceCharge = 200,
                Availability = true
            };

            bool result = repository.AddService(service);
            Console.WriteLine(result ? "Service added successfully." : "Failed to add service.");
        }
        #endregion

        #region TestAddBooking
        public static void TestAddBooking()
        {
            var booking = new Booking
            {
                ServiceId = 1000,
                UserId = 1000,
                AddressId = 1000,
                AttendantId = 1001,
                DateOfService = DateOnly.FromDateTime(DateTime.Today).AddDays(3),
                DeepCleaning = true,
                RepeatFrequency = 1,
                Instructions = "Cover electronics",
                CustomerSupplies = "Gloves",
                ServiceStatus = "Scheduled"
            };

            bool result = repository.AddBooking(booking);
            Console.WriteLine(result ? "Booking added successfully." : "Failed to add booking.");
        }
        #endregion

        #region TestAddAddress
        public static void TestAddAddress()
        {
            var address = new Address
            {
                UserId = 1000,
                HouseNumber = 909,
                AreaName = "Lake View",
                City = "Boston",
                PinCode = 110007
            };

            bool result = repository.AddAddress(address);
            Console.WriteLine(result ? "Address added successfully." : "Failed to add address.");
        }
        #endregion

        #region TestAddPayment
        public static void TestAddPayment()
        {
            var payment = new Payment
            {
                BookingId = 1000,
                PaymentType = "UPI",
                CardHolderName = null,
                CardNumber = null,
                Cvv = null,
                UpiId = "alice@upi"
            };

            bool result = repository.AddPayment(payment);
            Console.WriteLine(result ? "Payment added successfully." : "Failed to add payment.");
        }
        #endregion

        #region TestAddFeedback
        public static void TestAddFeedback()
        {
            var feedback = new Feedback
            {
                BookingId = 1000,
                Ratings = 5,
                Message = "Excellent work!"
            };

            bool result = repository.AddFeedback(feedback);
            Console.WriteLine(result ? "Feedback added successfully." : "Failed to add feedback.");
        }
        #endregion

        #region TestUpdateService
        public static void TestUpdateService()
        {
            var service = new Service
            {
                ServiceId = 1000,
                ServiceType = "Indoor",
                ServiceName = "Mopping and Polishing",
                Description = "Deep floor cleaning with polish",
                Duration = 3,
                SuppliesRequired = "Mop, Polish",
                ServiceCharge = 180,
                Availability = true
            };

            bool result = repository.UpdateService(service);
            Console.WriteLine(result ? "Service updated successfully." : "Failed to update service.");
        }
        #endregion

        #region TestGetServices
        public static void TestGetServices()
        {
            string serviceType = "Indoor";
            var services = repository.GetServices(serviceType);
            foreach (var s in services)
            {
                Console.WriteLine($"{s.ServiceId}: {s.ServiceName}, {s.Description}, Charge: {s.ServiceCharge}");
            }
        }
        #endregion

        #region TestGetBookings
        public static void TestGetBookings()
        {
            var bookings = repository.GetBookings(1000);
            foreach (var b in bookings)
            {
                Console.WriteLine($"{b.BookingId}: ServiceId={b.ServiceId}, Status={b.ServiceStatus}");
            }
        }
        #endregion

        #region TestGetFeedbacks
        public static void TestGetFeedbacks()
        {
            var feedbacks = repository.GetFeedbacks();
            foreach (var f in feedbacks)
            {
                Console.WriteLine($"{f.FeedbackId}: Rating={f.Ratings}, Message={f.Message}");
            }
        }
        #endregion

        #region TestGetUserDetails
        public static void TestGetUserDetails()
        {
            string email = "john@example.com";
            string password = "pass123";

            var user = repository.GetUserDetails(email, password);
            if (user != null)
                Console.WriteLine($"Welcome, {user.FirstName} {user.LastName} ({user.Role})");
            else
                Console.WriteLine("Invalid login.");
        }
        #endregion

        #region TestRemoveUser
        public static void TestRemoveUser()
        {
            short userId = 1004;
            bool result = repository.RemoveUser(userId);
            Console.WriteLine(result ? "User removed successfully." : "Failed to remove user.");
        }
        #endregion

        #region TestRemoveService
        public static void TestRemoveService()
        {
            short serviceId = 1003;
            bool result = repository.RemoveService(serviceId);
            Console.WriteLine(result ? "Service removed successfully." : "Failed to remove service.");
        }
        #endregion

        #region TestRemoveBooking
        public static void TestRemoveBooking()
        {
            short bookingId = 1003;
            bool result = repository.RemoveBooking(bookingId);
            Console.WriteLine(result ? "Booking removed successfully." : "Failed to remove booking.");
        }
        #endregion

        #region TestRemoveFeedback
        public static void TestRemoveFeedback()
        {
            short feedbackId = 1003;
            bool result = repository.RemoveFeedback(feedbackId);
            Console.WriteLine(result ? "Feedback removed successfully." : "Failed to remove feedback.");
        }
        #endregion
    }
}
