import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Employee, Contract, Presence, Leave, Payroll, Training } from '../models/data.models';

@Injectable({
  providedIn: 'root'
})
export class BackendApiService {
  private base = environment.apiUrl.replace(/\/$/, '') + '/api';

  constructor(private http: HttpClient) {}

  // Employees
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.base}/employees`);
  }

  getEmployee(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.base}/employees/${id}`);
  }

  addEmployee(e: Employee): Observable<Employee> {
    return this.http.post<Employee>(`${this.base}/employees`, e);
  }

  updateEmployee(id: number, e: Employee): Observable<any> {
    return this.http.put(`${this.base}/employees/${id}`, e);
  }

  deleteEmployee(id: number): Observable<any> {
    return this.http.delete(`${this.base}/employees/${id}`);
  }

  // Contracts
  getContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(`${this.base}/contracts`);
  }

  // Presences
  getPresences(): Observable<Presence[]> {
    return this.http.get<Presence[]>(`${this.base}/presences`);
  }

  // Leaves
  getLeaves(): Observable<Leave[]> {
    return this.http.get<Leave[]>(`${this.base}/leaves`);
  }

  // Payroll
  getPayroll(): Observable<Payroll[]> {
    return this.http.get<Payroll[]>(`${this.base}/payrolls`);
  }

  // Trainings
  getTrainings(): Observable<Training[]> {
    return this.http.get<Training[]>(`${this.base}/trainings`);
  }
}
