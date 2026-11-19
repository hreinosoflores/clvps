import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { SpinnerComponent } from '../../../shared/spinner/spinner.component';
import { Usuario } from '../../../models/Usuario';
import { OrdenPagoService } from '../../../services/orden-pago.service';


@Component({
  selector: 'app-login',
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, ReactiveFormsModule, MatProgressSpinnerModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {

  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  private _showPassword = signal(false);
  private dialog = inject(MatDialog);


  private authService = inject(AuthService);
  private ordenService = inject(OrdenPagoService);

  showPassword = computed(() => this._showPassword());
  loginStatus = computed(() => this.authService.loginStatus());
  usuarioActual = computed(() => this.authService.usuarioActual());
  idOrdenActual = computed(() => this.ordenService.idOrdenActual());

  loginForm = this.formBuilder.group({
    userName: ['', Validators.required],
    password: ['', Validators.required]
  });

  error: string = '';

  handleShowPassword(event: MouseEvent) {
    this._showPassword.set(!this._showPassword());
    event.preventDefault();
    event.stopPropagation();
  }

  submit(): void {
    if (this.loginForm.valid) {
      this.error = '';
      this.authService.setLoginStatus(2);
      const dialogRef = this.dialog.open(SpinnerComponent, {
        disableClose: true
      });

      const request: Usuario = {
        userName: this.loginForm.value["userName"]!,
        password: this.loginForm.value["password"]!
      };

      this.authService.login(request).subscribe({
        next: (resp: Usuario) => {
          console.log('Login Ok', resp);
          this.setLocalStorage(resp);
          this.authService.setUsuario(resp);
          this.authService.setLoginStatus(3);
          dialogRef.close();

          if (this.idOrdenActual()) {
            this.router.navigate(['/pago/detalle'])
          } else {
            this.router.navigate(['/profile/mis-datos']);
          }
        },
        error: (error) => {
          this.authService.setUsuario(null);
          this.authService.setLoginStatus(1);
          dialogRef.close();
          this.error = error.error.errorMessage;
        }
      });
    }
  }


  setLocalStorage(usu: Usuario) {
    const { userName, nombreCompleto, rol } = usu;
    localStorage.setItem('userName', userName!);
    localStorage.setItem('nombreCompleto', nombreCompleto!);
    localStorage.setItem('rol', rol!);
  }

}
