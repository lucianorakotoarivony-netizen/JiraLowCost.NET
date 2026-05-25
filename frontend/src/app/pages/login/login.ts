import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../Services/auth';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private auth = inject(Auth);
  username = signal<string>('');
  password = signal<string>('');
  errorStatus = signal<boolean>(false);
  errorMessage = signal<string | null>(null);

  onSubmit(): void{
    this.errorStatus.set(false);
    this.auth.login(this.username(), this.password()).subscribe({
      next:(response) => this.auth.saveSession(response),
      error:(err) => {
        this.errorStatus.set(true);
        this.errorMessage.set(err?.error?.message || "Une erreur est survenue")
      }
    })
  }
}
