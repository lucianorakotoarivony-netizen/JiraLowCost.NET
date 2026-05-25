import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthResponse, CreateTaskItem, TaskItem, UserResponse } from '../models/site.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Data {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;
  taskItemListData = signal<TaskItem[]>([]);
  taskItemDetailData = signal<TaskItem | null>(null);
  UserListData = signal<UserResponse[]>([]);
  errorMessage = signal<string | null>(null);
  errorStatus = signal<boolean>(false);

  private fetchAndSetData<T>(url: string, target: WritableSignal<T | null>, initialValue: T | null, params?: Record<string, string>){
    this.errorStatus.set(false);
    this.errorMessage.set(null);
    target.set(initialValue);
    this.http.get<T>(`${this.apiUrl}/${url}`, {params}).subscribe({
      next: (data) => {
        target.set(data);
      },
      error: (err) => {
        this.errorStatus.set(true);
        let message = err?.error && typeof err.error.message === "string" 
        ? err.error.message : 'Une erreur est survenue. Veuillez réesayer.';
        this.errorMessage.set(message);
      }
    });
  }

  createTaskItem(dto: CreateTaskItem): Observable<TaskItem>{
    return this.http.post<TaskItem>(`${this.apiUrl}/taskitem`, dto);
  }

  getUsers(): Observable<UserResponse[]>{
    return this.http.get<UserResponse[]>(`${this.apiUrl}/user`)
  }
  
  loadTaskItemList(filter?: string): void{
    const params: Record<string, string> = {};
    if (filter) params['filter'] = filter;
    this.fetchAndSetData<TaskItem[]>("taskitem", this.taskItemListData, [], params);
  }
  loadTaskItemDetail(id: string): void{
    this.fetchAndSetData<TaskItem>(`taskitem/${id}`, this.taskItemDetailData, null);
  }
  loadTasksForDev(filter?: string): void {
  const params: Record<string, string> = { dashboard: 'dev' };
  if (filter) params['filter'] = filter;
  this.fetchAndSetData<TaskItem[]>('taskitem', this.taskItemListData, [], params);
}
}
