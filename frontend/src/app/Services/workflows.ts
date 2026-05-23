import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { TaskItem } from '../models/site.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Workflows {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;
  takeTask(id: number): Observable<TaskItem>{
    return this.http.patch<TaskItem>(`${this.apiUrl}/taskitem/${id}/take`,{});
  }
  abandonTask(id: number): Observable<TaskItem>{
    return this.http.patch<TaskItem>(`${this.apiUrl}/taskitem/${id}/abandon`,{});
  }
  finishTask(id: number): Observable<TaskItem>{
    return this.http.patch<TaskItem>(`${this.apiUrl}/taskitem/${id}/finish`, {});
  }
  declineTask(id: number): Observable<TaskItem>{
    return this.http.patch<TaskItem>(`${this.apiUrl}/taskitem/${id}/decline`,{});
  }
  acceptTask(id: number): Observable<TaskItem>{
    return this.http.patch<TaskItem>(`${this.apiUrl}/taskitem/${id}/accept`,{});
  }
}
