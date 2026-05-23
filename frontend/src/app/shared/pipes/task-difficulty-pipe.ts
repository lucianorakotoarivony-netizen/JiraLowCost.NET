import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'taskDifficulty',
})
export class TaskDifficultyPipe implements PipeTransform {
  private readonly labels : Record<string, string> = {
    LEAD: "Lead level",
    SENIOR: "Senior level",
    MID: "Mid level",
    JUNIOR: "Junior level"
  };
  transform(value: string | null | undefined): string {
    if (!value) return "Inconnu";
    return this.labels[value] || value;    
  }
}
