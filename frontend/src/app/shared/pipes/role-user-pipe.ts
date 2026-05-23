import { Pipe, PipeTransform } from '@angular/core';
import { USER_ROLE } from '../../../Constants/user-role';

@Pipe({
  name: 'roleUser',
})
export class RoleUserPipe implements PipeTransform {
    private readonly labels : Record<string, string> = {
    PO: "Product Owner",
    LEAD: "Lead Dev",
    SENIOR: "Senior Dev",
    MID: "Mid Dev",
    JUNIOR: "Junior Dev"
  };
  transform(value: string | null | undefined): string {
    if (!value) return "Inconnu";
    return this.labels[value] || value;    
  }
}
