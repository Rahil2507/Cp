import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User, Service, Booking, Address, Payment, Feedback } from './models';

@Injectable({
  providedIn: 'root'
})
export class CleanNShineService {
  private baseUrl = 'https://localhost:44325/api/CleanNShine';

  constructor(private http: HttpClient) {}

  // AddUser
  addUser(user: User): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/AddUser`, user).pipe(catchError(this.errorHandler));
  }

  // AddService
  addService(service: Service): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/AddService`, service).pipe(catchError(this.errorHandler));
  }

  // AddBooking
  addBooking(booking: Booking): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/AddBooking`, booking).pipe(catchError(this.errorHandler));
  }

  // AddAddress
  addAddress(address: Address): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/AddAddress`, address).pipe(catchError(this.errorHandler));
  }

  // AddPayment
  addPayment(payment: Payment): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/AddPayment`, payment).pipe(catchError(this.errorHandler));
  }

  // AddFeedback
  addFeedback(feedback: Feedback): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/AddFeedback`, feedback).pipe(catchError(this.errorHandler));
  }

  // UpdateService
  updateService(service: Service): Observable<boolean> {
    return this.http.put<boolean>(`${this.baseUrl}/UpdateService`, service).pipe(catchError(this.errorHandler));
  }

  // GetServices
  getServices(): Observable<Service[]> {
    return this.http.get<Service[]>(`${this.baseUrl}/GetServices`).pipe(catchError(this.errorHandler));
  }

  // GetBookings
  getBookings(userId: number): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.baseUrl}/GetBookings?userId=${userId}`).pipe(catchError(this.errorHandler));
  }

  // GetFeedbacks
  getFeedbacks(serviceId: number): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${this.baseUrl}/GetFeedbacks?serviceId=${serviceId}`).pipe(catchError(this.errorHandler));
  }

  // GetUserDetails
  getUserDetails(userId: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/GetUserDetails?userId=${userId}`).pipe(catchError(this.errorHandler));
  }

  // RemoveUser
  removeUser(userId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/RemoveUser?userId=${userId}`).pipe(catchError(this.errorHandler));
  }

  // RemoveService
  removeService(serviceId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/RemoveService?serviceId=${serviceId}`).pipe(catchError(this.errorHandler));
  }

  // RemoveBooking
  removeBooking(bookingId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/RemoveBooking?bookingId=${bookingId}`).pipe(catchError(this.errorHandler));
  }

  // RemoveFeedback
  removeFeedback(feedbackId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/RemoveFeedback?feedbackId=${feedbackId}`).pipe(catchError(this.errorHandler));
  }

  // Error handler
  private errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(() => error.message || 'Server Error');
  }
}
