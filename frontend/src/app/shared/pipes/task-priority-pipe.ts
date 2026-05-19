import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'taskPriority',
})
export class TaskPriorityPipe implements PipeTransform {
  private readonly labels : Record<string, string> = {
    low: "Faible",
    medimum: "Moyenne",
    high: "Haute",
  };
  transform(value: string | null | undefined): string {
    if (!value) return "Inconnu";
    return this.labels[value] || value;    
  }
}
