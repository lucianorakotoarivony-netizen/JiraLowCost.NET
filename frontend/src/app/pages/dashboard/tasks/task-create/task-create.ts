import { Component, inject, OnInit, signal} from '@angular/core';
import { Data } from '../../../../Services/data';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TASKS_ITEM_PRIORITY } from '../../../../../Constants/task-item-priority';
import { TASK_ITEM_DIFFICULTY } from '../../../../../Constants/task-item-difficulty';
import { UserResponse } from '../../../../models/site.models';
import { RoleUserPipe } from '../../../../shared/pipes/role-user-pipe';
import { userKeyRole } from '../../../../../Constants/user-role';
import { TaskDifficultyPipe } from '../../../../shared/pipes/task-difficulty-pipe';
import { TaskPriorityPipe } from '../../../../shared/pipes/task-priority-pipe';



@Component({
  selector: 'app-task-create',
  imports: [ReactiveFormsModule, RoleUserPipe, TaskDifficultyPipe, TaskPriorityPipe],
  templateUrl: './task-create.html',
  styleUrl: './task-create.scss',
})
export class TaskCreate implements OnInit{
  dataService = inject(Data);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  users = signal<UserResponse[]>([]);
  difficulties = Object.values(TASK_ITEM_DIFFICULTY);
  priorities = Object.values(TASKS_ITEM_PRIORITY);
  errorStatus = signal<boolean>(false);
  errorMessage = signal<string | null>(null);
  userId = signal<string | null>(null);

  taskForm : FormGroup = this.fb.group({
    title:['', Validators.required],
    description: [''],
    difficulty: [TASK_ITEM_DIFFICULTY.SENIOR],
    priority: [TASKS_ITEM_PRIORITY.MEDIUM],
    assignedToId: [""],
  });
  ngOnInit(): void{
    this.errorMessage.set(null);
    this.errorStatus.set(false);
    this.dataService.getUsers().subscribe({
      next:(users) => this.users.set(users),
      error: (err) => {
        this.errorStatus.set(true);
        let message = err?.error && typeof err.error.error === "string" 
        ? err.error.message : 'Une erreur est survenue. Veuillez réesayer.';
        this.errorMessage.set(message);
      }
    });
  }

  onSubmit(): void{
    if (this.taskForm.invalid) return;
    const dto = { ...this.taskForm.value};
    if(!dto.assignedTo) delete dto.assignedTo;
    console.log(dto);
    this.dataService.createTaskItem(dto).subscribe({
      next:() => this.router.navigate(['/dashboard/taskitem']),
      error:(err) => {
        console.log(err);
        this.errorStatus.set(true);
        let message = err?.error && typeof err.error === "string" 
        ? err.error : 'Une erreur est survenue. Veuillez réesayer.';
        this.errorMessage.set(message);
      }
    })
  }

  onAssigneeChange(userId: string): void {
  if (!userId) {
    // Réinitialise à la valeur par défaut
    this.taskForm.get('difficulty')?.setValue(TASK_ITEM_DIFFICULTY.SENIOR);
    return;
  }

  const selectedUser = this.users().find(u => u.id === userId);
  if (selectedUser) {
    const roleToDifficulty: Partial<Record<userKeyRole, string>> = {
      JUNIOR: TASK_ITEM_DIFFICULTY.JUNIOR,
      MID: TASK_ITEM_DIFFICULTY.MID,
      SENIOR: TASK_ITEM_DIFFICULTY.SENIOR,
      LEAD: TASK_ITEM_DIFFICULTY.LEAD,
    };
    const userRoleKey = selectedUser.role as userKeyRole;
    const difficulty = roleToDifficulty[userRoleKey];
    this.taskForm.get('difficulty')?.setValue(difficulty);
  }
}
onDifficultyChange(){
  const userId = this.taskForm.get('assignedToId')?.value;
  const user = this.users().find(u => u.id === userId);
  const difficulty = this.taskForm.get('difficulty')?.value;
  const difficultyKey = Object
    .keys(TASK_ITEM_DIFFICULTY)
    .find(k => 
      TASK_ITEM_DIFFICULTY[k as keyof typeof TASK_ITEM_DIFFICULTY] === difficulty);
  if (difficultyKey !== user?.role){
    this.taskForm.get("assignedToId")?.setValue("");
  }
}
}
