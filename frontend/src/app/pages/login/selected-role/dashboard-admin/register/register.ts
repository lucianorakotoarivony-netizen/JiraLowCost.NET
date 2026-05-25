import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { USER_ROLE } from '../../../../../../Constants/user-role';
import { Auth } from '../../../../../Services/auth';
import { RoleUserPipe } from '../../../../../shared/pipes/role-user-pipe';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RoleUserPipe],
  templateUrl: './register.html',
  styleUrls: ['./register.scss']
})
export class Register {
  private fb = inject(FormBuilder);

  private auth = inject(Auth);

  errorStatus = signal(false);
  errorMessage = signal<string | null>(null);

  registerForm: FormGroup = this.fb.group({
    username: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    role: [USER_ROLE.JUNIOR]
  });

  roles = Object.values(USER_ROLE).filter(r => r !== USER_ROLE.PO); // Le PO ne crée pas d'autre PO

  onSubmit(): void {
    if (this.registerForm.invalid) return;
    this.auth.register(this.registerForm.value).subscribe({
      next: () => this.registerForm.reset(),
      error: (err) => {
        this.errorStatus.set(true);
        this.errorMessage.set(err?.error?.message || 'Erreur lors de la création.');
      }
    });
  }
}