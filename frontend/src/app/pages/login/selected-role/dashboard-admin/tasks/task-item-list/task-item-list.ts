import { Component, inject, OnInit, signal } from "@angular/core";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { TaskStatusPipe } from "../../../../../../shared/pipes/task-status-pipe";
import { DatePipe } from "@angular/common";
import { TaskPriorityPipe } from "../../../../../../shared/pipes/task-priority-pipe";
import { TaskDifficultyPipe } from "../../../../../../shared/pipes/task-difficulty-pipe";
import { Data } from "../../../../../../Services/data";
import { Auth } from "../../../../../../Services/auth";
import { FILTER } from "../../../../../../../Constants/filter";


@Component({
  selector: 'app-task-item-list',
  imports: [RouterLink, TaskStatusPipe, DatePipe, TaskPriorityPipe, TaskDifficultyPipe],
  templateUrl: './task-item-list.html',
  styleUrl: './task-item-list.scss',
})
export class TaskItemList implements OnInit{
  dataService = inject(Data);
  auth = inject(Auth);
  route = inject(ActivatedRoute);
  router = inject(Router);
  filter = FILTER;
  tasks = this.dataService.taskItemListData;
  errorMessage = this.dataService.errorMessage;
  errorStatus = this.dataService.errorStatus;
  activeFilter = signal<string | null>(null);
  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      const filter = params.get('filter');
      this.dataService.loadTaskItemList(filter ?? undefined);
    });
  }
  setFilter(filter: string): void{
    this.activeFilter.set(filter);
    this.router.navigate([],{
      queryParams:{filter},
      queryParamsHandling:'merge',
      replaceUrl: true,
    });
    this.dataService.loadTaskItemList(filter);
  }
}
