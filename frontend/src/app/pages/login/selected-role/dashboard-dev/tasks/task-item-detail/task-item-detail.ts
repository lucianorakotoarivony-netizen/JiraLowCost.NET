import { DatePipe } from "@angular/common";
import { RouterLink } from "@angular/router";
import { TaskStatusPipe } from "../../../../../../shared/pipes/task-status-pipe";
import { TaskPriorityPipe } from "../../../../../../shared/pipes/task-priority-pipe";
import { RoleUserPipe } from "../../../../../../shared/pipes/role-user-pipe";
import { TaskDifficultyPipe } from "../../../../../../shared/pipes/task-difficulty-pipe";
import { BaseDetail } from "../../../../../../../base-detail";
import { TaskItem } from "../../../../../../models/site.models";
import { Auth } from "../../../../../../Services/auth";
import { Component, inject } from "@angular/core";
import { TASK_ITEM_STATUS } from "../../../../../../../Constants/task-item-status";
import { Workflows } from "../../../../../../Services/workflows";
import { Observable, take } from "rxjs";


@Component({
  selector: 'app-task-item-detail',
  imports: [DatePipe, TaskStatusPipe, RouterLink, TaskPriorityPipe, RoleUserPipe, TaskDifficultyPipe],
  templateUrl: './task-item-detail.html',
  styleUrl: './task-item-detail.scss',
})
export class TaskItemDetail extends BaseDetail<TaskItem>{
  auth = inject(Auth);
  taskStatus = TASK_ITEM_STATUS;
  detailData = this.dataService.taskItemDetailData ;
  listData = this.dataService.taskItemListData ;
  currentUserRole = this.auth.currentUserRole();
  currentUsername = this.auth.currentUsername();
  workflow = inject(Workflows);
  backRoute = 'selected-role/dashboard-dev/taskitem';

  loadDetail(id: string): void {
    this.dataService.loadTaskItemDetail(id);
  }
  loadList(): void {
    const filter = this.route.snapshot.queryParamMap.get('filter');
    this.dataService.loadTaskItemList(filter ?? undefined);
  }

  private actionTask(f: Observable<TaskItem>){
    this.errorStatus.set(false);
    this.errorMessage.set(null);
    f.subscribe({
      next: () => {
        this.route.paramMap.pipe(take(1)).subscribe(params => {
          const id = params.get('id');
          if (id) this.loadDetail(id);
        });
      },
      error:(err) => {
        this.errorStatus.set(true);
        console.log(err)
        this.errorMessage.set(err?.error?.message || 'Une erreur est survenue');
      }
    })
  };
  
  takeTask(id: number): void{
    this.actionTask(this.workflow.takeTask(id));
  };
  abandonTask(id: number): void{
    this.actionTask(this.workflow.abandonTask(id));
  };
  finishTask(id: number): void{
    this.actionTask(this.workflow.finishTask(id));
  }
  declineTask(id: number): void{
    this.actionTask(this.workflow.declineTask(id));
  }
  acceptTask(id: number): void{
    this.actionTask(this.workflow.acceptTask(id));
  }
}
