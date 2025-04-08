using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanNShine.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CleanNShine.DAL
{
    public class CleanNShineRepository
    {
        private CleanNshineDbContext context;

        public CleanNShineRepository(CleanNshineDbContext context)
        {
            this.context = context;
        }

        public User GetUserDetails(string email, string password)
        {
            User userDetails;
            try
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                if (user != null && user.Password == password)
                {
                    userDetails = user;
                }
                else
                {
                    userDetails = new User();
                }
            }
            catch (Exception e)
            {
                userDetails = new User();
                Console.WriteLine(e.Message);
            }

            return userDetails;
        }

        public List<Service> GetServices(string serviceType)
        {
            List<Service> services;

            SqlParameter serviceType_param = new SqlParameter("@serviceType", serviceType);

            try
            {
                services = context.Services
                    .FromSqlRaw("SELECT * FROM ufn_GetAllServices(@serviceType)", serviceType_param)
                    .ToList();
            }
            catch (Exception e)
            {
                services = new List<Service>();
                Console.WriteLine(e.Message);
            }

            return services;
        }

        public List<Booking> GetBookings(short userId)
        {
            List<Booking> bookings;
            try
            {
                bookings = context.Bookings
                    .Where(b => b.UserId == userId)
                    .ToList();
            }
            catch (Exception e)
            {
                bookings = new List<Booking>();
                Console.WriteLine(e.Message);
            }

            return bookings;
        }

        public List<Feedback> GetFeedbacks()
        {
            List<Feedback> feedbacks;
            try
            {
                feedbacks = context.Feedbacks
                    .FromSqlRaw("SELECT * FROM ufn_GetFeedbacks()")
                    .ToList();
            }
            catch (Exception e)
            {
                feedbacks = new List<Feedback>();
                Console.WriteLine(e.Message);
            }

            return feedbacks;
        }

        public bool AddUser(User user)
        {
            bool status;
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool AddService(Service service)
        {
            bool status;
            try
            {
                var result = context.Database.ExecuteSqlInterpolated(
                    $@"EXEC usp_AddService 
                    @ServiceType = {service.ServiceType}, 
                    @ServiceName = {service.ServiceName}, 
                    @Description = {service.Description}, 
                    @Duration = {service.Duration}, 
                    @SuppliesRequired = {service.SuppliesRequired}, 
                    @ServiceCharge = {service.ServiceCharge}, 
                    @Availability = {service.Availability}");

                status = result > 0;
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool AddBooking(Booking booking)
        {
            bool status;
            try
            {
                context.Bookings.Add(booking);
                context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool AddAddress(Address address)
        {
            bool status;
            try
            {
                context.Addresses.Add(address);
                context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool AddPayment(Payment payment)
        {
            bool status;
            try
            {
                context.Payments.Add(payment);
                context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool AddFeedback(Feedback feedback)
        {
            bool status;
            try
            {
                context.Feedbacks.Add(feedback);
                context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool UpdateService(Service updatedService)
        {
            bool status;
            try
            {
                var service = context.Services.Find(updatedService.ServiceId);
                if (service != null)
                {
                    service.ServiceType = updatedService.ServiceType;
                    service.ServiceName = updatedService.ServiceName;
                    service.Description = updatedService.Description;
                    service.Duration = updatedService.Duration;
                    service.SuppliesRequired = updatedService.SuppliesRequired;
                    service.ServiceCharge = updatedService.ServiceCharge;
                    service.Availability = updatedService.Availability;

                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool RemoveUser(short userId)
        {
            bool status;
            try
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool RemoveService(short serviceId)
        {
            bool status;
            try
            {
                var service = context.Services.FirstOrDefault(s => s.ServiceId == serviceId);
                if (service != null)
                {
                    context.Services.Remove(service);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool RemoveBooking(short bookingId)
        {
            bool status;
            try
            {
                var booking = context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                if (booking != null)
                {
                    context.Bookings.Remove(booking);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }

        public bool RemoveFeedback(short feedbackId)
        {
            bool status;
            try
            {
                var feedback = context.Feedbacks.FirstOrDefault(f => f.FeedbackId == feedbackId);
                if (feedback != null)
                {
                    context.Feedbacks.Remove(feedback);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return status;
        }


    }
}
