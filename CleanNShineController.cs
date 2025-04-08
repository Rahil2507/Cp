using CleanNShine.DAL;
using CleanNShine.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CleanNShine.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CleanNShineController : Controller
    {
        CleanNshineDbContext context;
        CleanNShineRepository repository;
        public CleanNShineController(CleanNShineRepository repository)
        {
            context = new CleanNshineDbContext();
            this.repository = repository;
        }


        [HttpGet]
        public JsonResult GetUserDetails(string emailId, string password)
        {
            User usersDetails;
            try
            {
                usersDetails = repository.GetUserDetails(emailId, password);
            }
            catch (Exception e) 
            {
                usersDetails = new User();
                Console.WriteLine(e.Message);
            }

            return Json(usersDetails);
        }

        [HttpGet]
        public JsonResult GetServices(string serviceType)
        {
            List<Service> servicesList;
            try
            {
                servicesList = repository.GetServices(serviceType);
            }
            catch (Exception e)
            {
                servicesList = [];
                Console.WriteLine(e.Message);
            }

            return Json(servicesList);
        }

        [HttpGet]
        public JsonResult GetBookings(short userId)
        {
            List<Booking> bookingList;
            try
            {
                bookingList = repository.GetBookings(userId);
            }
            catch (Exception e)
            {
                bookingList = [];
                Console.WriteLine(e.Message);
            }

            return Json(bookingList);
        }

        [HttpGet]
        public JsonResult GetFeedbacks()
        {
            List<Feedback> feedbackList;
            try
            {
                feedbackList = repository.GetFeedbacks();
            }
            catch (Exception e)
            {
                feedbackList = [];
                Console.WriteLine(e.Message);
            }

            return Json(feedbackList);
        }

        [HttpPost]
        public JsonResult AddUser(User userObj)
        {
            var status = false;

            User userObject = new User();
            userObject.FirstName = userObj.FirstName;
            userObject.LastName = userObj.LastName;
            userObject.Email = userObj.Email;
            userObject.Contact = userObj.Contact;
            userObject.Password = userObj.Password;
            userObject.DateOfBirth = userObj.DateOfBirth;
            userObject.Gender = userObj.Gender;
            userObject.Role = userObj.Role;

            try
            {
                status = repository.AddUser(userObject);
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpPost]
        public JsonResult AddService(Service serviceObj)
        {
            var status = false;
            
            Service serviceObject = new Service();
            serviceObject.ServiceType = serviceObj.ServiceType;
            serviceObject.ServiceName = serviceObj.ServiceName;
            serviceObject.Description = serviceObj.Description;
            serviceObject.Duration = serviceObj.Duration;
            serviceObject.SuppliesRequired = serviceObj.SuppliesRequired;
            serviceObject.ServiceCharge = serviceObj.ServiceCharge;
            serviceObject.Availability = serviceObj.Availability;

            try
            {
                status = repository.AddService(serviceObject);
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpPost]
        public JsonResult AddBooking(Booking bookingObj)
        {
            bool status = false;

            Booking bookingObject = new Booking();
            bookingObject.ServiceId = bookingObj.ServiceId;
            bookingObject.UserId = bookingObj.UserId;
            bookingObject.AddressId = bookingObj.AddressId;
            bookingObject.AttendantId = bookingObj.AttendantId;
            bookingObject.DateOfService = bookingObj.DateOfService;
            bookingObject.DeepCleaning = bookingObj.DeepCleaning;
            bookingObject.RepeatFrequency = bookingObj.RepeatFrequency;
            bookingObject.Instructions = bookingObj.Instructions;
            bookingObject.CustomerSupplies = bookingObj.CustomerSupplies;
            bookingObject.ServiceStatus = bookingObj.ServiceStatus;
            
            try
            {
                status = repository.AddBooking(bookingObject);
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpPost]
        public JsonResult AddAddress(Address addressObj)
        {
            bool status = false;

            Address addressObject = new Address();
            addressObject.UserId = addressObj.UserId;
            addressObject.HouseNumber = addressObj.HouseNumber;
            addressObject.AreaName = addressObj.AreaName;
            addressObject.City = addressObj.City;
            addressObject.PinCode = addressObj.PinCode;

            try
            {
                status = repository.AddAddress(addressObject);
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpPost]
        public JsonResult AddPayment(Payment paymentObj)
        {
            bool status = false;

            Payment paymentObject = new Payment();
            paymentObject.BookingId = paymentObj.BookingId;
            paymentObject.PaymentType = paymentObj.PaymentType;
            paymentObject.CardHolderName = paymentObj.CardHolderName;
            paymentObject.CardNumber = paymentObj.CardNumber;
            paymentObject.Cvv = paymentObj.Cvv;
            paymentObject.UpiId = paymentObj.UpiId;

            try
            {
                status = repository.AddPayment(paymentObject);
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpPost]
        public JsonResult AddFeedback(Feedback feedbackObj)
        {
            bool status = false;

            Feedback feedbackObject = new Feedback();
            feedbackObject.BookingId = feedbackObj.BookingId;
            feedbackObject.Ratings = feedbackObj.Ratings;
            feedbackObject.Message = feedbackObj.Message;

            try
            {
                status = repository.AddFeedback(feedbackObject);
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpPut]
        public JsonResult UpdateService(Service serviceObj)
        {
            bool status = false;

            Service serviceObject = new Service();
            serviceObject.ServiceId = serviceObj.ServiceId;
            serviceObject.ServiceType = serviceObj.ServiceType;
            serviceObject.ServiceName = serviceObj.ServiceName;
            serviceObject.Description = serviceObj.Description;
            serviceObject.Duration = serviceObj.Duration;
            serviceObject.SuppliesRequired = serviceObj.SuppliesRequired;
            serviceObject.ServiceCharge = serviceObj.ServiceCharge;
            serviceObject.Availability = serviceObj.Availability;
            try
            {
                status =  repository.UpdateService(serviceObject);
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        

        [HttpDelete]
        public JsonResult RemoveUser(short userId)
        {
            bool status = false;
            try 
            { 
                status = repository.RemoveUser(userId); 
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpDelete]
        public JsonResult RemoveService(short serviceId)
        {
            bool status = false;
            try 
            {
                status = repository.RemoveService(serviceId); 
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpDelete]
        public JsonResult RemoveBooking(short bookingId)
        {
            bool status = false;
            try 
            {
                status = repository.RemoveBooking(bookingId); 
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }

        [HttpDelete]
        public JsonResult RemoveFeedback(short feedbackId)
        {
            bool status = false;
            try 
            {
                status = repository.RemoveFeedback(feedbackId); 
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }

            return Json(status);
        }
    }
}
