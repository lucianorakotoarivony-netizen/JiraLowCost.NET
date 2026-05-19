import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'taskStatus',
})
export class TaskStatusPipe implements PipeTransform {
  private readonly labels : Record<string, string> = {
    todo: "A faire",
    in_progress: "En cours",
    done: "Terminé",
    pending:"En attente",
  };
  transform(value: string | null | undefined): string {
    if (!value) return "Inconnu";
    return this.labels[value] || value;    
  }
  }
