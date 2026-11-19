import { Component, computed, inject } from '@angular/core';
import { AvatarComponent } from "../avatar/avatar.component";
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-sidebar',
  imports: [AvatarComponent, MatDividerModule, MatListModule, MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  private router = inject(Router);
  private authService = inject(AuthService);

  usuarioActual = computed(() => this.authService.usuarioActual());

  goToPerfil() {
    this.router.navigate(['/profile']);
  }

  logout() {
    localStorage.clear();
    this.authService.setUsuario(null);
    this.authService.setLoginStatus(1);
    this.router.navigate(['/']).then(() => {
      window.location.reload();
    });
    console.log('logout');
  }



}
