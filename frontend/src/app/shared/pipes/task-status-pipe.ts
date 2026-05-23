import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'taskStatus',
})
export class TaskStatusPipe implements PipeTransform {
  private readonly labels : Record<string, string> = {
    TODO: "A faire",
    IN_PROGRESS: "En cours",
    DONE: "Terminé",
    PENDING:"En attente",
  };
  transform(value: string | null | undefined): string {
    if (!value) return "Inconnu";
    return this.labels[value] || value;    
  }
  }
